using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Screens.Models
{
    public class Image
    {
       public int imageID { get; set; }
        [Required]
        public int imageOrder{ get; set; }
        [Required ,DefaultValue("0"),ForeignKey(nameof(screen))]
        public int imageScreenId { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال عنوان الصورة"), MaxLength(200)]
        public string imageTitle { get; set; }
        [ MaxLength(200),AllowNull]
        public string imageDescription { get; set; }

        [MaxLength(100)]
        public string imageBath { get; set; }

        public DateTime imagefromDate { get; set; } = DateTime.UtcNow;
        public DateTime imagetoDate { get;set ; } = DateTime.UtcNow;
        public int image_status { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "الرجاء اختيار الصورة")]
        public IFormFile ImageFile { get; set; }

        
        public Screen screen { get; set; }

    }
}
