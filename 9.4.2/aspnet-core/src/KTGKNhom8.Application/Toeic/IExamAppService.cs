using Abp.Application.Services;
using System.Threading.Tasks;

namespace KTGKNHOM8.Toeic
{
    public interface IExamAppService : IApplicationService
    {
        // Nhận file dưới dạng mảng byte để tránh phụ thuộc vào IFormFile của tầng Web
        Task CreateExamFromWordAsync(byte[] fileBytes, string fileName);
    }
}