using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models; // Đảm bảo rằng bạn đã thêm thư viện chứa model NhaCungCap
using System.Collections.Generic;
using System.Linq;

namespace ProjectA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhaCungCapController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ: Hiển thị danh sách nhà cung cấp
        public async Task<IActionResult> Index()
        {
            var nhaCungCap = await _db.NhaCungCap.ToListAsync();
            if (nhaCungCap == null || !nhaCungCap.Any())
            {
                nhaCungCap = new List<NhaCungCap>(); // Tạo danh sách rỗng để tránh lỗi null
            }

            return View(nhaCungCap);
        }

        // CREATE: Hiển thị form tạo nhà cung cấp mới
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Lưu nhà cung cấp mới vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NhaCungCap obj)
        {
            if (ModelState.IsValid)
            {
                _db.NhaCungCap.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // EDIT: Hiển thị form chỉnh sửa nhà cung cấp
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var nhaCungCapFromDb = _db.NhaCungCap.Find(id);

            if (nhaCungCapFromDb == null)
            {
                return NotFound();
            }

            return View(nhaCungCapFromDb);
        }

        // EDIT: Lưu thay đổi nhà cung cấp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NhaCungCap obj)
        {
            if (ModelState.IsValid)
            {
                _db.NhaCungCap.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // DELETE: Xóa nhà cung cấp khỏi database qua HTTP GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var nhaCungCap = _db.NhaCungCap.Find(id);

            if (nhaCungCap == null)
            {
                return NotFound();
            }

            _db.NhaCungCap.Remove(nhaCungCap);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DETAILS: Hiển thị chi tiết nhà cung cấp
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var nhaCungCapFromDb = _db.NhaCungCap.Find(id);

            if (nhaCungCapFromDb == null)
            {
                return NotFound();
            }

            return View(nhaCungCapFromDb);
        }
    }
}
