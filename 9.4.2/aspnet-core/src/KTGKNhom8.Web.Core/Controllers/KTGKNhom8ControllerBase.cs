using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace KTGKNhom8.Controllers
{
    public abstract class KTGKNhom8ControllerBase: AbpController
    {
        protected KTGKNhom8ControllerBase()
        {
            LocalizationSourceName = KTGKNhom8Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
