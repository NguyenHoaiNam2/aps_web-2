using System.ComponentModel.DataAnnotations;

namespace BaiTap07a.Models
{
    public class NhaCungCap
    {
        [Key]
        public int Id { get; set; }  // Khóa chính

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được dài quá 100 ký tự.")]
        public string Ten { get; set; }  // Tên nhà cung cấp

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được dài quá 200 ký tự.")]
        public string DiaChi { get; set; }  // Địa chỉ nhà cung cấp

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng!")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được dài quá 15 ký tự.")]
        public string SDT { get; set; }  // Số điện thoại nhà cung cấp
    }
}
