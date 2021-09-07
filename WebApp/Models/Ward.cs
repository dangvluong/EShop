using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Ward
    {
        public short WardId { get; set; }
        public string WardName { get; set; }
        public short ProvinceId { get; set; }
        public short DistrictId { get; set; }
    }
}
