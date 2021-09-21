using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SizeOfProductRepository :BaseRepository
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
