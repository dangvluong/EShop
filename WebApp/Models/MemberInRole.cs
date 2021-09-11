using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MemberInRole
    {
        public Guid MemberId { get; set; }
        public byte RoleId { get; set; }
    }
}
