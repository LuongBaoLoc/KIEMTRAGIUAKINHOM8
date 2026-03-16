using System.Threading.Tasks;
using Abp.Configuration.Startup;
using KTGKNhom8.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace KTGKNhom8.Web.Views.Shared.Components.RightNavbarUserArea
{
    public class RightNavbarUserAreaViewComponent : KTGKNhom8ViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public RightNavbarUserAreaViewComponent(
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RightNavbarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return View(model);
        }
    }
}

