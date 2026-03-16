using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using KTGKNhom8.Controllers;

namespace KTGKNhom8.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : KTGKNhom8ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
