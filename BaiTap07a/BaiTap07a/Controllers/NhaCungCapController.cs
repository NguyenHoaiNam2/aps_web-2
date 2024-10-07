using BaiTap07a.Data;
using BaiTap07a.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BaiTap07a.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhaCungCapController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Hiển thị form thêm nhà cung cấp
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Thêm nhà cung cấp vào cơ sở dữ liệu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _db.NhaCungCap.Add(nhaCungCap);   // Thêm nhà cung cấp mới
                _db.SaveChanges();               // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction(nameof(Index));  // Chuyển hướng về trang danh sách nhà cung cấp
            }
            return View(nhaCungCap);  // Quay lại form nếu có lỗi
        }

        // GET: Hiển thị danh sách nhà cung cấp
        public IActionResult Index()
        {
            var listNhaCungCap = _db.NhaCungCap.ToList();  // Lấy danh sách nhà cung cấp
            return View(listNhaCungCap);                   // Trả về view hiển thị danh sách
        }

        // GET: Hiển thị form chỉnh sửa nhà cung cấp
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var nhaCungCap = _db.NhaCungCap.Find(id);  // Tìm nhà cung cấp theo ID
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);  // Trả về form chỉnh sửa
        }

        // POST: Cập nhật thông tin nhà cung cấp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _db.NhaCungCap.Update(nhaCungCap);   // Cập nhật nhà cung cấp
                _db.SaveChanges();                  // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction(nameof(Index));  // Chuyển hướng về danh sách nhà cung cấp
            }
            return View(nhaCungCap);  // Nếu có lỗi, quay lại form
        }

        // GET: Hiển thị trang xác nhận xóa nhà cung cấp
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var nhaCungCap = _db.NhaCungCap.Find(id);  // Tìm nhà cung cấp theo ID
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);  // Trả về trang xác nhận xóa
        }

        // POST: Xác nhận xóa nhà cung cấp
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nhaCungCap = _db.NhaCungCap.Find(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            _db.NhaCungCap.Remove(nhaCungCap);  // Xóa nhà cung cấp
            _db.SaveChanges();                  // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction(nameof(Index));  // Chuyển hướng về danh sách nhà cung cấp
        }

        // GET: Hiển thị chi tiết nhà cung cấp
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var nhaCungCap = _db.NhaCungCap.Find(id);  // Tìm nhà cung cấp theo ID
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);  // Trả về view chi tiết
        }
    }
}
