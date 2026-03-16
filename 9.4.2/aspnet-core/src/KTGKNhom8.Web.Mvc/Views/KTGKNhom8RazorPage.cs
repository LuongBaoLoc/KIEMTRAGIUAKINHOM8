using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace KTGKNhom8.Web.Views
{
    public abstract class KTGKNhom8RazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected KTGKNhom8RazorPage()
        {
            LocalizationSourceName = KTGKNhom8Consts.LocalizationSourceName;
        }
    }
}
