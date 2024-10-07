using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiKiemTra03_02.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; } // Mã tác giả (author id)

        [Required(ErrorMessage = "Tên tác giả không được để trống!")]
        public string Name { get; set; } // Tên tác giả (author name)

        public string Nationality { get; set; } // Quốc tịch (nationality)

        public int BirthYear { get; set; } // Năm sinh (birth year)
    }
}
