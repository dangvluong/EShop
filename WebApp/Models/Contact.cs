using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Contact
    {
        public short ContactId { get; set; }
        public string AddressHome { get; set; }
        public short ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public short DistrictId { get; set; }
        public string DistrictName { get; set; }
        public short WardId { get; set; }
        public string WardName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }      
    }
}
