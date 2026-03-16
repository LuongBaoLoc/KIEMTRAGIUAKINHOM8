using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using KTGKNhom8.Controllers;

namespace KTGKNhom8.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : KTGKNhom8ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
