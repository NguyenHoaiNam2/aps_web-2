using Microsoft.AspNetCore.Mvc;
using ProjectA.Data;
using ProjectA.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProjectA.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // Lấy danh sách sản phẩm và bao gồm thông tin thể loại
            IEnumerable<SanPham> sanPham = _db.SanPham.Include("TheLoai").ToList();
            return View(sanPham);
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult NhaCungCap()
        {
            // Lấy danh sách nhà cung cấp
            var nhaCungCapList = _db.NhaCungCap.ToList();
            return View(nhaCungCapList);
        }

        public IActionResult DetailsNhaCungCap(int id)
        {
            var nhaCungCap = _db.NhaCungCap.Find(id);

            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        public IActionResult Danhsach(string sortOrder, string searchString)
        {
            // Lấy danh sách sản phẩm và bao gồm thông tin thể loại
            IEnumerable<SanPham> sanPham = _db.SanPham.Include("TheLoai").ToList();

            // Tìm kiếm theo tên sản phẩm
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPham = sanPham.Where(s => s.Name.Contains(searchString));
            }

            // Sắp xếp theo tên hoặc giá
            switch (sortOrder)
            {
                case "name_desc":
                    sanPham = sanPham.OrderByDescending(s => s.Name);
                    break;
                case "name_asc": // Xử lý sắp xếp theo tên A - Z
                    sanPham = sanPham.OrderBy(s => s.Name);
                    break;
                case "price":
                    sanPham = sanPham.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    sanPham = sanPham.OrderByDescending(s => s.Price);
                    break;
                default:
                    sanPham = sanPham.OrderByDescending(s => s.Name); // Mặc định sắp xếp theo tên Z - A
                    break;
            }

            return View(sanPham);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Details(int sanphamId)
        {
            // Lấy thông tin sản phẩm hiện tại
            var sanPham = _db.SanPham
                .Include(sp => sp.TheLoai)
                .Include(sp => sp.NhaCungCap)
                .FirstOrDefault(sp => sp.Id == sanphamId);

            if (sanPham == null)
            {
                return NotFound(); // Trả về NotFound nếu sản phẩm không tồn tại
            }

            // Lấy danh sách sản phẩm cùng thể loại
            var sanPhamsCungTheLoai = _db.SanPham
                .Where(sp => sp.TheLoaiId == sanPham.TheLoaiId && sp.Id != sanphamId)
                .ToList();

            // Tạo mô hình cho view
            var viewModel = new SanPhamDetailsViewModel
            {
                SanPham = sanPham,
                SanPhamsCungTheLoai = sanPhamsCungTheLoai,
                Quantity = 1 // Khởi tạo số lượng mặc định là 1
            };

            return View(viewModel);
        }




        [HttpPost]
        [Authorize]
        public IActionResult Details(GioHang giohang)
        {
            // Lấy thông tin tài khoản
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value;

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng của người dùng hay chưa
            var giohangdb = _db.GioHang.FirstOrDefault(gh => gh.SanPhamId == giohang.SanPhamId
                && gh.ApplicationUserId == giohang.ApplicationUserId);

            if (giohangdb == null)
            {
                // Nếu sản phẩm chưa có trong giỏ, thêm mới vào giỏ hàng
                _db.GioHang.Add(giohang);
            }
            else
            {
                // Nếu sản phẩm đã có trong giỏ hàng, chỉ cần tăng số lượng sản phẩm
                giohangdb.Quantity += giohang.Quantity;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            // Chuyển hướng về trang danh sách sản phẩm hoặc giỏ hàng
            return RedirectToAction("Danhsach");
        }


        [HttpGet]
        public IActionResult FilterByTheLoai(int id)
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai")
                .Where(sp => sp.TheLoai.Id == id)
                .ToList();
            return View("Danhsach", sanpham);
        }
    }
}
