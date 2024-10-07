using BaiKiemTra03_02.Data;
using BaiKiemTra03_02.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra03_02.Controllers
{
    public class TheLoaiController : Controller
    {

        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ: Hiển thị danh sách thể loại
        public IActionResult Index()
        {
            var theloai = _db.TheLoais.ToList();
            ViewBag.TheLoai = theloai;

            // Kiểm tra nếu không có dữ liệu
            if (theloai == null || !theloai.Any())
            {
                ViewBag.TheLoai = new List<TheLoai>(); // Đặt danh sách rỗng để tránh NullReferenceException
            }

            return View();
        }

        // CREATE: Hiển thị form tạo thể loại mới
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Lưu thể loại mới vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TheLoai obj)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoais.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // EDIT: Hiển thị form chỉnh sửa thể loại
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var theloaiFromDb = _db.TheLoais.Find(id);

            if (theloaiFromDb == null)
            {
                return NotFound();
            }

            return View(theloaiFromDb);
        }

        // EDIT: Lưu thay đổi thể loại
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TheLoai obj)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoais.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // DELETE: Hiển thị form xác nhận xóa thể loại
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var theloaiFromDb = _db.TheLoais.Find(id);

            if (theloaiFromDb == null)
            {
                return NotFound();
            }

            return View(theloaiFromDb);
        }

        // DELETE: Xóa thể loại khỏi database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var theloai = _db.TheLoais.Find(id);

            if (theloai == null)
            {
                return NotFound();
            }

            _db.TheLoais.Remove(theloai);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DETAILS: Hiển thị chi tiết thể loại
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var theloaiFromDb = _db.TheLoais.Find(id);

            if (theloaiFromDb == null)
            {
                return NotFound();
            }

            return View(theloaiFromDb);
        }
    }
}
