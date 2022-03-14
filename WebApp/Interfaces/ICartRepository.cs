using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ICartRepository
    {
        int Add(Cart obj);
        ICollection<Cart> GetCarts(Guid cartId);
        int Edit(Cart obj);
        int Delete(Cart obj);

    }
}
