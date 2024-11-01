using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;

namespace ProjectA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<HoaDon> hoadon = _db.HoaDon.Include("ApplicationUser").ToList();
            return View(hoadon);
        }
        // Action to display details of a specific order
        public IActionResult ChiTietDonHang(int hoaDonId)
        {
            var chiTietHoaDon = _db.ChiTietHoaDon
                                   .Where(c => c.HoaDonId == hoaDonId)
                                   .ToList();

            if (!chiTietHoaDon.Any())
            {
                return NotFound($"No details found for HoaDonId {hoaDonId}");
            }

            return View(chiTietHoaDon); // Ensure the correct view is set up
        }
    }
}