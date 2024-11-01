using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Danh sách sản phẩm
        public IActionResult Index()
        {
            // Lấy thông tin từ bảng SanPham, bao gồm thông tin từ bảng TheLoai và NhaCungCap
            var sanPhamList = _db.SanPham
                .Include(sp => sp.TheLoai)
                .Include(sp => sp.NhaCungCap)
                .ToList();

            return View(sanPhamList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            // Khởi tạo đối tượng SanPham mới
            SanPham sanPham = new SanPham();

            // Lấy danh sách thể loại và nhà cung cấp
            ViewBag.DsTheLoai = _db.TheLoai.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }
            ).ToList();

            ViewBag.DsNhaCungCap = _db.NhaCungCap.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }
            ).ToList();

            if (id == null || id == 0) // Thêm mới sản phẩm
            {
                return View(sanPham);
            }
            else // Chỉnh sửa sản phẩm
            {
                sanPham = _db.SanPham
                    .Include(sp => sp.TheLoai)
                    .Include(sp => sp.NhaCungCap)
                    .FirstOrDefault(sp => sp.Id == id);

                if (sanPham == null)
                {
                    return NotFound();
                }

                return View(sanPham);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.Id == 0)
                {
                    // Thêm sản phẩm mới vào cơ sở dữ liệu
                    _db.SanPham.Add(sanpham);
                }
                else
                {
                    // Cập nhật sản phẩm hiện tại
                    _db.SanPham.Update(sanpham);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _db.SaveChanges();

                // Chuyển về trang danh sách sản phẩm
                return RedirectToAction("Index");
            }

            // Cập nhật danh sách thể loại và nhà cung cấp trong trường hợp ModelState không hợp lệ
            ViewBag.DsTheLoai = _db.TheLoai.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }
            ).ToList();

            ViewBag.DsNhaCungCap = _db.NhaCungCap.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                }
            ).ToList();

            return View(sanpham);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            if (sanpham == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            // Xóa sản phẩm khỏi cơ sở dữ liệu
            _db.SanPham.Remove(sanpham);
            _db.SaveChanges();

            return Json(new { success = true, message = "Xóa thành công." });
        }
    }
}
