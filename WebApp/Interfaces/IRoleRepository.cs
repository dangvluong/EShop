using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> GetRolesByMember(Guid memberId);

    }
}
