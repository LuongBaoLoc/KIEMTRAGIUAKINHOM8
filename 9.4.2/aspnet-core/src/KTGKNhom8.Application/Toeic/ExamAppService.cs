using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI; // Thư viện chứa UserFriendlyException
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTGKNHOM8.Toeic
{
    public class ExamAppService : ApplicationService, IExamAppService
    {
        // Dependency Injection các Repository của ASP.NET Boilerplate
        private readonly IRepository<Exam> _examRepository;
        private readonly IRepository<ExamPart> _partRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;

        public ExamAppService(
            IRepository<Exam> examRepository,
            IRepository<ExamPart> partRepository,
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository)
        {
            _examRepository = examRepository;
            _partRepository = partRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public async Task CreateExamFromWordAsync(byte[] fileBytes, string fileName)
        {
            // Unit of Work của ABP sẽ tự động theo dõi hàm này. 
            // Nếu có UserFriendlyException được throw, nó sẽ TỰ ĐỘNG ROLLBACK.

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                using (var wordDoc = WordprocessingDocument.Open(memoryStream, false))
                {
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    // Lấy toàn bộ text từ file word, phân cách bằng dấu xuống dòng
                    string fullText = string.Join("\n", body.Elements<Paragraph>().Select(p => p.InnerText));

                    // 1. Quét thông tin chung của Đề thi [cite: 46-47]
                    var titleMatch = Regex.Match(fullText, @"\[EXAM_TITLE\](.*)");
                    if (!titleMatch.Success) throw new UserFriendlyException("Lỗi Format: Thiếu thẻ [EXAM_TITLE]");

                    var timeMatch = Regex.Match(fullText, @"\[EXAM_TIME\]\s*(\d+)");
                    if (!timeMatch.Success) throw new UserFriendlyException("Lỗi Format: Thiếu thẻ [EXAM_TIME]");

                    // Lưu Exam vào Database và lấy Id vừa tạo
                    var examId = await _examRepository.InsertAndGetIdAsync(new Exam
                    {
                        Title = titleMatch.Groups[1].Value.Trim(),
                        TimeLimit = int.Parse(timeMatch.Groups[1].Value.Trim()),
                        Description = "Được import từ file: " + fileName
                    });

                    // 2. Quét Part 5 [cite: 48]
                    var part5Match = Regex.Match(fullText, @"\[PART:5\](.*?)(\[PART:6\]|$)", RegexOptions.Singleline);
                    if (part5Match.Success)
                    {
                        var partId = await _partRepository.InsertAndGetIdAsync(new ExamPart { ExamId = examId, PartType = 5 });
                        
                        // tất cả các khối câu hỏi trong Part 5 (từ [Q:xxx] đến [KEY:x]) [cite: 49-54]
                        var qMatches = Regex.Matches(part5Match.Value, @"\[Q:(\d+)\](.*?)\[KEY:([A-D])\]", RegexOptions.Singleline);
                        
                        foreach (Match qMatch in qMatches)
                        {
                            var qNumber = int.Parse(qMatch.Groups[1].Value);
                            var qBody = qMatch.Groups[2].Value; // Nội dung câu hỏi và 4 đáp án
                            var correctKey = qMatch.Groups[3].Value.Trim().ToUpper(); // A, B, C, D

                            // Lấy text câu hỏi (nằm trước chữ [A])
                            var qContentMatch = Regex.Match(qBody, @"^(.*?)(?=\[A\])", RegexOptions.Singleline);
                            string qText = qContentMatch.Success ? qContentMatch.Groups[1].Value.Trim() : "Lỗi đọc nội dung";

                            // Lưu Câu hỏi
                            var questionId = await _questionRepository.InsertAndGetIdAsync(new Question
                            {
                                ExamPartId = partId,
                                QuestionNumber = qNumber,
                                Content = qText,
                                IsShuffle = true // Mặc định cho phép đảo
                            });

                            // Lưu 4 đáp án [cite: 50-53]
                            string[] labels = { "A", "B", "C", "D" };
                            foreach (var label in labels)
                            {
                                var ansMatch = Regex.Match(qBody, $@"\[{label}\](.*?)(?=\[[A-D]\]|$)", RegexOptions.Singleline);
                                if (!ansMatch.Success) throw new UserFriendlyException($"Lỗi Format: Câu {qNumber} thiếu đáp án [{label}]");

                                await _answerRepository.InsertAsync(new Answer
                                {
                                    QuestionId = questionId,
                                    Label = label,
                                    Content = ansMatch.Groups[1].Value.Trim(),
                                    IsCorrect = (label == correctKey)
                                });
                            }
                        }
                    }
                    
                    // Code quét Part 6 và Part 7 (có [PASSAGE_START]) sẽ tương tự, 
                    // bạn có thể dựa vào khung Regex này để mở rộng thêm sau.
                }
            }
        }
    }
}