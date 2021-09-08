using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Member
    {
        public Guid MemberId { get; set; }        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public bool Remember { get; set; }
        public int? DefaultContact { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
