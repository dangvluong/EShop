using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ProductRepository : BaseRepository
    {
        //IConfiguration configuration;
        public ProductRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }
        public IEnumerable<Product> GetRandom12Products()
        {
            return connection.Query<Product>("GetRandom12Products", commandType: CommandType.StoredProcedure);
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
        public IEnumerable<Product> GetAllProduct()
        {
            return connection.Query<Product>("SELECT * FROM Product WHERE IsDeleted = 0");
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
        public ICollection<Product> SearchProduct(string query, int page, int size, out int total)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Query", "%" + query + "%");
            parameters.Add("@Page", page, dbType: DbType.Int32);
            parameters.Add("@Size", size, dbType: DbType.Int32);
            parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            ICollection<Product> products = (ICollection<Product>)connection.Query<Product>("SearchProduct", parameters, commandType: CommandType.StoredProcedure);
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
        public short Add(Product obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", dbType: DbType.Int16, direction: ParameterDirection.Output);
            parameters.Add("@ProductName", obj.ProductName);
            parameters.Add("@Sku", obj.Sku);
            parameters.Add("@Price", obj.Price,dbType: DbType.Int32);
            parameters.Add("@PriceSaleOff", obj.PriceSaleOff, dbType: DbType.Int32);
            parameters.Add("@Material", obj.Material);
            parameters.Add("@Description", obj.Description);
            connection.Execute("AddProduct",parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<short>("@ProductId");
        }
        public int Delete(short id)
        {
            return connection.Execute($"UPDATE Product SET IsDeleted = 1 WHERE ProductId = {id}");
        }
        public IEnumerable<Statistic> GetBestSellingProduct()
        {
            return connection.Query<Statistic>("GetBestSellingProducts", commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Statistic> GetTop5HighestInventoryProducts()
        {
            return connection.Query<Statistic>("GetHighestInventoryProducts", commandType: CommandType.StoredProcedure);
        }
        public List<Product> GetProductsRelation(short productId)
        {
            return connection.Query<Product>("GetProductsRelation", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
