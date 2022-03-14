using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới không trùng nhau")]
        public string RePassword { get; set; }
        public string Token { get; set; }
    }
}
