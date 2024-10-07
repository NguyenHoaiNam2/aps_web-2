using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra03_02.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; } // Mã sách (book id)

        [Required(ErrorMessage = "Tiêu đề sách không được để trống!")]
        public string Title { get; set; } // Tiêu đề sách (title)

        [Required(ErrorMessage = "Năm xuất bản không được để trống!")]
        public int PublicationYear { get; set; } // Năm xuất bản (publication year)

        [Required]
        public double Price { get; set; } // Giá sách

        public string? Description { get; set; } // Mô tả sách
        public string? ImageUrl { get; set; } // Đường dẫn hình ảnh sách

        [Required]
        public int AuthorId { get; set; } // Mã tác giả (author id)

        [ForeignKey("AuthorId")]
        [ValidateNever]
        public Author Author { get; set; } // Liên kết đến tác giả

        [Required]
        public int TheLoaiId { get; set; } // Mã thể loại (genre id)

        [ForeignKey("TheLoaiId")]
        [ValidateNever]
        public TheLoai TheLoai { get; set; } // Liên kết đến thể loại
    }
}
