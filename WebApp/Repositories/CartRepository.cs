using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class CartRepository : BaseRepository, ICartRepository
    {
        public CartRepository(IDbConnection connection) : base(connection) { }

        public int Add(Cart obj)
        {
            return connection.Execute("AddCart", new
            {
                CartId = obj.CartId,
                ProductId = obj.ProductId,
                ColorId = obj.ColorId,
                SizeId = obj.SizeId,
                Quantity = obj.Quantity,
                Price = obj.Price
            }, commandType: CommandType.StoredProcedure);
        }
        public ICollection<Cart> GetCarts(Guid cartId)
        {
            return (ICollection<Cart>)connection.Query<Cart>("GetCarts", new { CartId = cartId }, commandType: CommandType.StoredProcedure);
        }

        public int Edit(Cart obj)
        {
            return connection.Execute("EditCart", new
            {
                CartId = obj.CartId,
                ProductId = obj.ProductId,
                ColorId = obj.ColorId,
                SizeId = obj.SizeId,
                Quantity = obj.Quantity
            }, commandType: CommandType.StoredProcedure);
        }
        public int Delete(Cart obj)
        {
            return connection.Execute("DeleteCart", new
            {
                CartId = obj.CartId,
                ProductId = obj.ProductId,
                ColorId = obj.ColorId,
                SizeId = obj.SizeId               
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
