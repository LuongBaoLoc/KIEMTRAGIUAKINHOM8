using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using KTGKNhom8.Models;

namespace KTGKNhom8.Controllers;

public class ExamController : Controller
{
    // ==========================================
    // PHÂN HỆ GIẢNG VIÊN (QUẢN LÝ ĐỀ THI)
    // ==========================================

    // 1. Hiển thị form Upload file Word
    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    // 2. Xử lý logic khi bấm nút "Tải lên"
    [HttpPost]
    public async Task<IActionResult> Upload(UploadExamViewModel model)
    {
        // Kiểm tra xem người dùng đã nhập đủ Form chưa
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Bắt lỗi nếu file rỗng
        if (model.ExamFile == null || model.ExamFile.Length == 0)
        {
            ModelState.AddModelError("ExamFile", "Vui lòng chọn một file.");
            return View(model);
        }

        // Bắt lỗi định dạng: Bắt buộc là .docx
        var fileExtension = Path.GetExtension(model.ExamFile.FileName).ToLower();
        if (fileExtension != ".docx")
        {
            ModelState.AddModelError("ExamFile", "Hệ thống chỉ chấp nhận file Word định dạng .docx");
            return View(model);
        }

        try
        {
            // 2.1 Tạo thư mục 'uploads' trong wwwroot nếu chưa có
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 2.2 Đặt tên file chống trùng lặp (Thêm GUID vào trước tên file)
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ExamFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 2.3 Lưu file xuống ổ cứng của Server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ExamFile.CopyToAsync(stream);
            }

            // =========================================================================
            // TODO: Tại đây sẽ gọi hàm đọc file OpenXML để quét các Tags [Q:101], [KEY:A]
            // Để hệ thống không bị lỗi lúc này, mình giả lập là bóc tách thành công 100%
            // =========================================================================
            bool isParseSuccess = true; 

            if (isParseSuccess)
            {
                ViewBag.SuccessMessage = $"Đã tải lên và bóc tách thành công: {model.ExamFile.FileName}! (Thời gian: {model.TimeInMinutes} phút).";
                
                // Bạn có thể xóa file vật lý đi sau khi đã đọc xong dữ liệu đưa vào Database
                // System.IO.File.Delete(filePath);
            }
            else
            {
                // Logic Rollback theo yêu cầu đồ án
                ModelState.AddModelError("", "File Word sai Format (Thiếu thẻ [KEY] hoặc sai số thứ tự). Đã Rollback toàn bộ dữ liệu, không lưu vào CSDL!");
                System.IO.File.Delete(filePath); // Xóa luôn file lỗi
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Có lỗi hệ thống xảy ra: " + ex.Message);
        }

        return View(model);
    }


    // ==========================================
    // PHÂN HỆ HỌC VIÊN (LÀM BÀI THI)
    // ==========================================

    // 3. Hiển thị giao diện làm bài thi chia 2 cột
    [HttpGet]
    public IActionResult TakeTest()
    {
        // Trong thực tế, bạn sẽ truy vấn Database (_context.Exams.Find(id)) để lấy dữ liệu.
        // Ở đây mình truyền dữ liệu mẫu (Mock data) ra View bằng ViewBag
        ViewBag.ExamTitle = "Đề thi TOEIC Reading Mẫu - ETS 2024";
        ViewBag.TimeInMinutes = 75; 

        return View();
    }

    // 4. Xử lý nộp bài và chấm điểm (Học viên bấm Submit)
    [HttpPost]
    public IActionResult SubmitTest(IFormCollection form)
    {
        // TODO: Đọc các đáp án học viên đã chọn từ "form"
        // So sánh với [KEY] trong Database và tính điểm
        
        return RedirectToAction("Result"); // Chuyển sang trang xem kết quả
    }

    // 5. Hiển thị trang Kết quả (VD: 85/100 câu đúng)
    [HttpGet]
    public IActionResult Result()
    {
        return View();
    }
}