using Microsoft.AspNetCore.Mvc;
using BaiTap07a.Data;
using BaiTap07a.Models;
using System.IO;

namespace BaiTap07a.Controllers
{
    public class TheloaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheloaiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Hiển thị danh sách thể loại
        public IActionResult Index()
        {
            var theloaiList = _db.Theloai.ToList();
            ViewBag.Theloai = theloaiList;
            return View();
        }

        // GET: Hiển thị form tạo thể loại mới
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tạo thể loại mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Theloai theloai, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra và xử lý hình ảnh nếu có
                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(ImageUrl.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageUrl", "Chỉ chấp nhận file hình ảnh (.jpg, .jpeg, .png, .gif).");
                    }
                    else if (ImageUrl.Length > 2 * 1024 * 1024) // Giới hạn 2MB
                    {
                        ModelState.AddModelError("ImageUrl", "Kích thước file ảnh không được vượt quá 2MB.");
                    }
                    else
                    {
                        // Lưu file ảnh
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetRandomFileName() + extension);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageUrl.CopyTo(stream);
                        }
                        theloai.ImageUrl = $"/images/{Path.GetFileName(filePath)}"; // Lưu đường dẫn ảnh
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "Hình ảnh là bắt buộc.");
                }

                if (ModelState.IsValid)
                {
                    _db.Theloai.Add(theloai);   // Thêm thể loại mới
                    _db.SaveChanges();          // Lưu thay đổi vào DB
                    return RedirectToAction("Index");  // Chuyển hướng về danh sách thể loại
                }
            }
            return View(theloai);  // Nếu có lỗi, trả lại form cùng dữ liệu đã nhập
        }

        // GET: Hiển thị form chỉnh sửa thể loại
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var theloai = _db.Theloai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        // POST: Chỉnh sửa thể loại
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Theloai theloai, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(ImageUrl.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageUrl", "Chỉ chấp nhận file hình ảnh (.jpg, .jpeg, .png, .gif).");
                    }
                    else if (ImageUrl.Length > 2 * 1024 * 1024) // Giới hạn 2MB
                    {
                        ModelState.AddModelError("ImageUrl", "Kích thước file ảnh không được vượt quá 2MB.");
                    }
                    else
                    {
                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(theloai.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", theloai.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Lưu ảnh mới
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetRandomFileName() + extension);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageUrl.CopyTo(stream);
                        }
                        theloai.ImageUrl = $"/images/{Path.GetFileName(filePath)}"; // Cập nhật đường dẫn hình ảnh
                    }
                }
                else if (string.IsNullOrEmpty(theloai.ImageUrl))
                {
                    ModelState.AddModelError("ImageUrl", "Hình ảnh là bắt buộc.");
                }

                if (ModelState.IsValid)
                {
                    _db.Theloai.Update(theloai);   // Cập nhật thông tin thể loại
                    _db.SaveChanges();             // Lưu thay đổi vào DB
                    return RedirectToAction("Index");  // Chuyển hướng về danh sách thể loại
                }
            }
            return View(theloai);  // Nếu có lỗi, trả lại form cùng dữ liệu đã nhập
        }

        // GET: Hiển thị trang xác nhận xóa thể loại
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var theloai = _db.Theloai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        // POST: Xác nhận xóa thể loại
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var theloai = _db.Theloai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }

            // Xóa ảnh nếu có
            if (!string.IsNullOrEmpty(theloai.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", theloai.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _db.Theloai.Remove(theloai);  // Xóa thể loại
            _db.SaveChanges();            // Lưu thay đổi vào DB
            return RedirectToAction("Index");
        }

        // GET: Hiển thị chi tiết thể loại
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var theloai = _db.Theloai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }

            return View(theloai);
        }

        [HttpGet]
      
        public IActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                // Sử dụng LINQ để tìm kiếm
                var Theloai = _db.Theloai.Where(tl => tl.Name.Contains(searchString)).ToList(); 
                ViewBag.TheLoai = Theloai;
            }
            else
            {
                var theloai = _db.Theloai.ToList(); ViewBag.TheLoai = theloai;
            }
           
            return View("Index"); // Sử dụng lại View Index
        }

    }
}
