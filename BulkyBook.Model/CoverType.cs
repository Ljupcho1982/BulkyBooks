using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Model
{
    public class CoverType
    {

        public int Id { get; set; }

        [Display(Name = "CoverType")]
        [Required]
        [MaxLength(50)]

        public string Name { get; set; }
    }
}
