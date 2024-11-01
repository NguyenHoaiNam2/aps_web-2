namespace ProjectA.Models
{
    public class SanPhamDetailsViewModel
    {
        public GioHang GioHang { get; set; } // Thêm thuộc tính GioHang
        public SanPham SanPham { get; set; }
        public List<SanPham> SanPhamsCungTheLoai { get; set; }
        public int Quantity { get; set; }
    }
}
