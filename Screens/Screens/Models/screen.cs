using System.ComponentModel.DataAnnotations;

namespace Screens.Models
{
    public class Screen
    {
        public int screenId { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال اسم الشاشة"), MaxLength(200)]
        public string screenName { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال وصف الصورة"), MaxLength(200)]
        public string screenDescription { get; set; }
        public bool  screenStatus { get; set; }
        public List<Image> Images { get; set; }
    }
}
