using System.Threading.Tasks;
using Abp.Application.Services;
using KTGKNhom8.Authorization.Accounts.Dto;

namespace KTGKNhom8.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
