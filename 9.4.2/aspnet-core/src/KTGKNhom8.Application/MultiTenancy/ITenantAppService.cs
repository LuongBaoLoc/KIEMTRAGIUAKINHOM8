using Abp.Application.Services;
using KTGKNhom8.MultiTenancy.Dto;

namespace KTGKNhom8.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

