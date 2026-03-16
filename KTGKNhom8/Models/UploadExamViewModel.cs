using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KTGKNhom8.Models;

public class UploadExamViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên đề thi")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập thời gian làm bài")]
    [Range(1, 200, ErrorMessage = "Thời gian phải từ 1 đến 200 phút")]
    public int TimeInMinutes { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn file đề thi")]
    public IFormFile ExamFile { get; set; }
}