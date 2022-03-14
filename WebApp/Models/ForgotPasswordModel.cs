using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Định dạng {0} không đúng")]
        public string Email { get; set; }
    }
}
