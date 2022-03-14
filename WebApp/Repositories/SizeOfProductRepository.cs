using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class SizeOfProductRepository :BaseRepository,ISizeOfProductRepository
    {
        public SizeOfProductRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<SizeOfProduct> list, short productId)
        {            
            connection.Execute($"DELETE FROM SizeOfProduct WHERE ProductId = {productId}");
            return connection.Execute("AddSizeOfProduct", list, commandType: CommandType.StoredProcedure);
        }
        public int Add(List<SizeOfProduct> list)
        {            
            return connection.Execute("AddSizeOfProduct", list, commandType: CommandType.StoredProcedure);
        }
    }
}
