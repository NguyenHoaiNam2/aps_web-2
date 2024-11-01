using Microsoft.AspNetCore.Mvc;
using ProjectA.Data;
using ProjectA.Models;
using System.Linq;

namespace ProjectA.ViewComponents
{
    public class TheLoaiViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public IViewComponentResult Invoke()
        {
            
            var theloai = _db.TheLoai.ToList();

    
            return View(theloai);
        }
    }
}
