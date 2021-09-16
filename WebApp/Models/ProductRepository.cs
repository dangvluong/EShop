using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductRepository : BaseRepository
    {
        //IConfiguration configuration;
        public ProductRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<Product> GetRandom10Product()
        {
            return connection.Query<Product>("GetRandom12Productcs", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Product> GetProducts(int page, int size, out int total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Page", page, dbType: DbType.Int32);
            parameters.Add("@Size", size, dbType: DbType.Int32);
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<Product> products = connection.Query<Product>("GetProducts", parameters, commandType: CommandType.StoredProcedure);
            total = parameters.Get<int>("@Total");
            return products;

        }
        public Product GetProductById(short id)
        {
            return connection.QueryFirstOrDefault<Product>("GetProductById", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Product> GetProductsByCategory(short categoryId, int page, int size, out int total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", categoryId, dbType: DbType.Int16);
            parameters.Add("@Page", page, dbType: DbType.Int32);
            parameters.Add("@Size", size, dbType: DbType.Int32);
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<Product> products = connection.Query<Product>("GetProductsByCategory", parameters, commandType: CommandType.StoredProcedure);
            total = parameters.Get<int>("@Total");
            return products;
        }

        public IEnumerable<Product> SearchProduct(string query, int page, int size, out int total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Query", "%" + query + "%");
            parameters.Add("@Page", page, dbType: DbType.Int32);
            parameters.Add("@Size", size, dbType: DbType.Int32);
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<Product> products = connection.Query<Product>("SearchProduct", parameters, commandType: CommandType.StoredProcedure);
            total = parameters.Get<int>("@Total");
            return products;
        }
        public int Edit(Product obj)
        {
            return connection.Execute("EditProduct", new
            {
                ProductId = obj.ProductId,
                ProductName = obj.ProductName,
                Sku = obj.Sku,
                Price = obj.Price,
                PriceSaleOff = obj.PriceSaleOff,
                Material = obj.Material,
                Description = obj.Description
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
