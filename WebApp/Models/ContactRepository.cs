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
        public int Add(Contact obj, Guid memberId)
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
        public Contact GetContactsById(short contactId)
        {
            return connection.QuerySingleOrDefault<Contact>("GetContactById", new { ContactId = contactId }, commandType: CommandType.StoredProcedure);
        }
        public int Update(Contact obj)
        {
            return connection.Execute("UpdateContact", new { 
            AddressHome = obj.AddressHome,
            ProvinceId = obj.ProvinceId,
            DistrictId = obj.DistrictId,
            WardId = obj.WardId,
            PhoneNumber= obj.PhoneNumber,
            FullName = obj.FullName,
            ContactId = obj.ContactId
            }, commandType: CommandType.StoredProcedure);
        }
        public int Delete(short id)
        {
            return connection.Execute("DeleteContact", new { ContactId = id }, commandType: CommandType.StoredProcedure);
        }

        public int UpdateDefaultContact(Guid memberId, short contactId)
        {
            return connection.Execute("UpdateDefaultContact", new { MemberId = memberId, ContactId = contactId}, commandType: CommandType.StoredProcedure);
        }
    }
}
