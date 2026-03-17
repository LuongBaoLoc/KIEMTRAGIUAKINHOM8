using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO; // Thêm thư viện này để dùng Path.GetExtension
using KTGKNhom8.Controllers;
using KTGKNhom8.ToeicExams;
using KTGKNhom8.ToeicExams.Dto; // Để dùng được ParsedExamDto
using KTGKNhom8.Toeic; // Thêm dòng này để Controller gọi được IExamAppService

namespace KTGKNhom8.Web.Controllers
{
    public class ExamController : KTGKNhom8ControllerBase
    {
        private readonly WordParserService _wordParserService;
        private readonly PdfParserService _pdfParserService; // Khai báo thêm Service đọc PDF
        private readonly IExamAppService _examAppService;

        // Tiêm cả 3 service vào Constructor
        public ExamController(
            WordParserService wordParserService,
            PdfParserService pdfParserService, 
            IExamAppService examAppService)
        {
            _wordParserService = wordParserService;
            _pdfParserService = pdfParserService;
            _examAppService = examAppService;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessUpload(IFormFile fileUpload)
        {
            if (fileUpload == null || fileUpload.Length == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn file hợp lệ.";
                return RedirectToAction("Upload");
            }

            try
            {
                // 1. Lấy đuôi file (ví dụ: .docx, .pdf, .png) chuyển về chữ thường để dễ so sánh
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                ParsedExamDto parsedExam = null;

                using (var stream = fileUpload.OpenReadStream())
                {
                    // 2. Kiểm tra đuôi file và gọi đúng "Chuyên gia" bóc tách
                    if (fileExtension == ".docx")
                    {
                        parsedExam = _wordParserService.ParseExamFromWord(stream);
                    }
                    else if (fileExtension == ".pdf")
                    {
                        parsedExam = _pdfParserService.ParseExamFromPdf(stream);
                    }
                    else if (fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".jpg")
                    {
                        // Chỗ này tạm thời chặn lại, vì đọc chữ từ ảnh cần tích hợp AI OCR rất phức tạp
                        TempData["ErrorMessage"] = "Hệ thống đang phát triển tính năng đọc ảnh (AI OCR). Vui lòng dùng Word hoặc PDF trước nhé!";
                        return RedirectToAction("Upload");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Hệ thống chỉ hỗ trợ bóc tách file Word (.docx) và PDF (.pdf).";
                        return RedirectToAction("Upload");
                    }

                    // 3. Nếu bóc tách thành công thì lưu vào Database
                    if (parsedExam != null)
                    {
                        await _examAppService.SaveParsedExamAsync(parsedExam);
                        TempData["SuccessMessage"] = $"Thành công! Đã bóc tách và lưu đề thi '{parsedExam.Title}' từ file {fileExtension} (Gồm {parsedExam.Questions.Count} câu hỏi).";
                    }
                }
                
                return RedirectToAction("Upload"); 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi xử lý file: " + ex.Message;
                return RedirectToAction("Upload");
            }
        }
    }
}