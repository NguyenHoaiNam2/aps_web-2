using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectA.Models
{
    public class NhaCungCap
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhà cung cấp không được vượt quá 100 ký tự.")]
        public string Name { get; set; } // Tên nhà cung cấp

        [Url(ErrorMessage = "Đường dẫn hình ảnh không hợp lệ.")]
        public string? ImageUrl { get; set; } // Đường dẫn ảnh nhà cung cấp (tùy chọn)

        [StringLength(500, ErrorMessage = "Tiểu sử không được vượt quá 500 ký tự.")]
        public string TieuSu { get; set; } // Tiểu sử nhà cung cấp

        [Required(ErrorMessage = "Ngày thành lập không được để trống!")]
        [DataType(DataType.Date)]
        public DateTime NgayThanhLap { get; set; } // Ngày thành lập
    }
}
