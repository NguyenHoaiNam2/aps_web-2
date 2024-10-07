using Microsoft.AspNetCore.Mvc;

namespace BaiTap04.Controllers
{
    public class NhomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
