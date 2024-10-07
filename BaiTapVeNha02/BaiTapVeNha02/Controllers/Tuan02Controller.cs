using Microsoft.AspNetCore.Mvc;

namespace BaiTapVeNha02.Controllers
{
    public class Tuan02Controller : Controller
    {
        public IActionResult Index()
        {
            ViewBag.HoTen = "Nguyen Hoai Nam"; 
            ViewBag.MSSV = "1822041128";     
            ViewBag.Nam = 2004;            

            return View();
        }

        public IActionResult Profile()
        {
            ViewBag.HoTen = "Nguyen Hoai Nam";
            ViewBag.MSSV = "1822041128";
            ViewBag.Nam = 2004;

            return View();
        }

        public IActionResult MayTinh(double a, double b, string pheptinh)
        {
            double ketQua = 0;
            string thongBao = "";

            switch (pheptinh)
            {
                case "cong":
                    ketQua = a + b;
                    thongBao = $"{a} + {b} = {ketQua}";
                    break;
                case "tru":
                    ketQua = a - b;
                    thongBao = $"{a} - {b} = {ketQua}";
                    break;
                case "nhan":
                    ketQua = a * b;
                    thongBao = $"{a} * {b} = {ketQua}";
                    break;
                case "chia":
                    if (b != 0)
                    {
                        ketQua = a / b;
                        thongBao = $"{a} / {b} = {ketQua}";
                    }
                    else
                    {
                        thongBao = "Lỗi: Không thể chia cho 0.";
                    }
                    break;
                default:
                    thongBao = "Lỗi: Phép tính không hợp lệ.";
                    break;
            }

            ViewBag.KetQua = thongBao;

            return View();
        }
    }
}
