using Abp.AspNetCore.Mvc.ViewComponents;

namespace KTGKNhom8.Web.Views
{
    public abstract class KTGKNhom8ViewComponent : AbpViewComponent
    {
        protected KTGKNhom8ViewComponent()
        {
            LocalizationSourceName = KTGKNhom8Consts.LocalizationSourceName;
        }
    }
}
