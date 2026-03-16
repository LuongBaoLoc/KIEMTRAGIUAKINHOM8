using Abp.AutoMapper;
using KTGKNhom8.Sessions.Dto;

namespace KTGKNhom8.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
