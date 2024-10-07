using Microsoft.AspNetCore.Mvc;
using BaiKiemTra03_02.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BaiKiemTra03_02.Data;

namespace BaiKiemTra03_02.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context; // Khởi tạo context cho việc truy cập cơ sở dữ liệu
        }

        // Hiển thị danh sách sách
        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.TheLoai).ToList(); // Lấy danh sách sách kèm theo thông tin tác giả và thể loại
            return View(books);
        }

        // Hiển thị trang thêm sách
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList(); // Lấy danh sách tác giả để hiển thị trong dropdown
            ViewBag.TheLoais = _context.TheLoais.ToList(); // Lấy danh sách thể loại để hiển thị trong dropdown
            return View();
        }

        // Xử lý thêm sách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu
            {
                _context.Books.Add(book); // Thêm sách vào cơ sở dữ liệu
                _context.SaveChanges(); // Lưu thay đổi
                return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách
            }
            ViewBag.Authors = _context.Authors.ToList(); // Nếu không hợp lệ, lấy lại danh sách tác giả
            ViewBag.TheLoais = _context.TheLoais.ToList(); // Lấy lại danh sách thể loại
            return View(book); // Trả về lại view với dữ liệu đã nhập
        }

        // Hiển thị trang sửa sách
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id); // Tìm sách theo id
            if (book == null)
            {
                return NotFound(); // Nếu không tìm thấy sách
            }
            ViewBag.Authors = _context.Authors.ToList(); // Lấy danh sách tác giả
            ViewBag.TheLoais = _context.TheLoais.ToList(); // Lấy danh sách thể loại
            return View(book); // Trả về view với sách
        }

        // Xử lý sửa sách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu
            {
                _context.Books.Update(book); // Cập nhật sách
                _context.SaveChanges(); // Lưu thay đổi
                return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách
            }
            ViewBag.Authors = _context.Authors.ToList(); // Nếu không hợp lệ, lấy lại danh sách tác giả
            ViewBag.TheLoais = _context.TheLoais.ToList(); // Lấy lại danh sách thể loại
            return View(book); // Trả về lại view với dữ liệu đã nhập
        }

        // Xử lý xóa sách
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id); // Tìm sách theo id
            if (book == null)
            {
                return NotFound(); // Nếu không tìm thấy sách
            }
            return View(book); // Trả về view xác nhận xóa
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id); // Tìm sách theo id
            if (book != null)
            {
                _context.Books.Remove(book); // Xóa sách
                _context.SaveChanges(); // Lưu thay đổi
            }
            return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách
        }
    }
}
