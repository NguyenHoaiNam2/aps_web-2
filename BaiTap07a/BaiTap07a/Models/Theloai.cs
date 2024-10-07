using System.ComponentModel.DataAnnotations;

namespace BaiTap07a.Models
{
    public class Theloai
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thể loại không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên thể loại không được dài quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ngày tạo không đúng!")]
        public DateTime DateTime { get; set; } = DateTime.Now;

       
        public string ImageUrl { get; set; }
    }
}
