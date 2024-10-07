using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapKiemTra01.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangKy(TaiKhoanModel model)
        {
            if (model != null && model.Password != null && (model.Password).Length > 0)
            {
                model.Password = model.Password + "_chuoi_ma_hoa";
            }


            return View(model);
           
        }

        public IActionResult SanPham()
        {
            ViewBag.name = "Táo";
            ViewBag.price = "200k";
           
            return View(SanPham);
        }

    }
}
