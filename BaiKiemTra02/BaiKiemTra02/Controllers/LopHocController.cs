using Microsoft.AspNetCore.Mvc;
using BaiKiemTra02.Models; 
using System.Linq; 
using System.Threading.Tasks;
using BaiKiemTra02.Data;

namespace BaiKiemTra02.Controllers
{
    public class LopHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public LopHocController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public IActionResult Index()
        {
            var lopHocs = _context.LopHoc.ToList();
            return View(lopHocs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lopHoc);
        }

   
        public IActionResult Details(int id)
        {
            var lopHoc = _context.LopHoc.FirstOrDefault(l => l.Id == id);
            if (lopHoc == null)
            {
                return NotFound();
            }
            return View(lopHoc);
        }

        // Hiển thị form chỉnh sửa LopHoc
        public IActionResult Edit(int id)
        {
            var lopHoc = _context.LopHoc.Find(id);
            if (lopHoc == null)
            {
                return NotFound();
            }
            return View(lopHoc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LopHoc lopHoc)
        {
            if (id != lopHoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lopHoc);
        }

        // Hiển thị xác nhận xóa LopHoc
        public IActionResult Delete(int id)
        {
            var lopHoc = _context.LopHoc.FirstOrDefault(l => l.Id == id);
            if (lopHoc == null)
            {
                return NotFound();
            }
            return View(lopHoc);
        }

        // Xử lý xóa LopHoc
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lopHoc = await _context.LopHoc.FindAsync(id);
            if (lopHoc != null)
            {
                _context.LopHoc.Remove(lopHoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
