using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using KTGKNHOM8.Toeic; // Khai báo namespace chứa IExamAppService
using KTGKNhom8.Controllers; // Base controller của ABP Boilerplate

namespace KTGKNhom8.Web.Controllers
{
    public class ExamController : KTGKNhom8ControllerBase
    {
        private readonly IExamAppService _examAppService;

        public ExamController(IExamAppService examAppService)
        {
            _examAppService = examAppService;
        }

        // 1. Hiển thị trang Upload
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // 2. Xử lý khi người dùng bấm nút "Bắt đầu Tải lên & Xử lý"
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile examFile)
        {
            if (examFile == null || examFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn file Word (.docx) để tải lên.";
                return View();
            }

            if (Path.GetExtension(examFile.FileName).ToLower() != ".docx")
            {
                TempData["ErrorMessage"] = "Hệ thống chỉ chấp nhận định dạng .docx";
                return View();
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await examFile.CopyToAsync(memoryStream);
                    // Gọi sang tầng Application để bóc tách file
                    await _examAppService.CreateExamFromWordAsync(memoryStream.ToArray(), examFile.FileName);
                }
                
                TempData["SuccessMessage"] = "Bóc tách và lưu đề thi thành công!";
                return RedirectToAction("Upload"); // Tạm thời load lại trang, sau này có thể chuyển sang trang Danh sách đề
            }
            catch (System.Exception ex)
            {
                // Bắt lỗi UserFriendlyException (sai thẻ Tags, thiếu đáp án...) và hiển thị cho Giảng viên
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}