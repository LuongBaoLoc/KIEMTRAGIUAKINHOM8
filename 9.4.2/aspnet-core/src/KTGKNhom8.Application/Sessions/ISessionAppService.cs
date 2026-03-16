using System.Threading.Tasks;
using Abp.Application.Services;
using KTGKNhom8.Sessions.Dto;

namespace KTGKNhom8.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
