using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ContactRepository : BaseRepository
    {
        public ContactRepository(IDbConnection connection) : base(connection) { }
        public int AddContact(Contact obj, Guid memberId)
        {
            return connection.Execute("AddContact", new
            {
                AddressHome = obj.AddressHome,
                ProvinceId = obj.ProvinceId,
                DistrictId = obj.DistrictId,
                WardId = obj.WardId,
                PhoneNumber = obj.PhoneNumber,
                FullName = obj.FullName,
                MemberId = memberId
            }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Contact> GetContactsByMember(Guid memberId)
        {
            return connection.Query<Contact>("GetContactsByMember", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }
    }
}
