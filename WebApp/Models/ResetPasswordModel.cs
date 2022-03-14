using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ResetPasswordModel
    {
        public string NewPassword { get; set; }
        public string RePassword { get; set; }
        public string Token { get; set; }
    }
}
