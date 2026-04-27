using System.ComponentModel.DataAnnotations;

namespace Screens.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "يرجى ادخال اسم المستخدم")]
        [Display(Name = "Employee No")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "يرجى ادخال كلمة المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
