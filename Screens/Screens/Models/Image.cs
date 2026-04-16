using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Screens.Models
{
    public class Image
    {
       public int imageID { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال عنوان الصورة"), MaxLength(200)]
        public string imageTitle { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال وصف الصورة"), MaxLength(200)]
        public string imageDescription { get; set; }
        [MaxLength(50)]
        public string imageBath { get; set; }
        public DateOnly imagefromDate { get; set; }
        public DateOnly imagetoDate { get;set ; }
        public int image_status { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
