using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string AddressHome { get; set; }
        public short ProvinceId { get; set; }
        public short DistrictId { get; set; }
        public short WardId { get; set; }
        public string PhoneNumber { get; set; }
        public int FullName { get; set; }                
    }
}
