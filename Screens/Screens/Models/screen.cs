using System.ComponentModel.DataAnnotations;

namespace Screens.Models
{
    public class screen
    {
        public int screenId { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال اسم الشاشة"), MaxLength(200)]
        public string screenName { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال وصف الصورة"), MaxLength(200)]
        public string screenDescription { get; set; }
        public bool  screenStatus { get; set; }


    }
}
