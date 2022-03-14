using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IContactRepository
    {
        int Add(Contact obj, Guid memberId);
        IEnumerable<Contact> GetContactsByMember(Guid memberId);
        Contact GetContactById(short contactId);
        int Update(Contact obj);
        int Delete(short contactId);    
        int UpdateDefaultContact(Guid memberId, short contactId);
    }
}
