namespace ProjectA.Models
{
    public class GioHangViewModel
    {
        public IEnumerable<GioHang> DsGioHang { get; set; }
        // Đã được lưu trữ trong HoaDon
        // public double Total { get; set; }
        public HoaDon HoaDon { get; set; }
    }
}
