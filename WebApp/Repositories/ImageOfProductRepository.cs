using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ImageOfProductRepository : BaseRepository,IImageOfProductRepository
    {
        //IConfiguration configuration;
        public ImageOfProductRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }
        public List<ImageOfProduct> GetImagesByProduct(short productId)
        {
            return connection.Query<ImageOfProduct>("GetImagesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public int GetNumberImageExists(ImageOfProductUpload obj)
        {
            return connection.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM ImageOfProduct WHERE ProductId = {obj.ProductId} AND ColorId = {obj.ColorId}");
        }
        public int AddImageOfProduct(ImageOfProductUpload obj, string imageUrl)
        {
            return connection.Execute("AddImageOfProduct", new { ProductId = obj.ProductId, ColorId = obj.ColorId, ImageUrl = imageUrl }, commandType: CommandType.StoredProcedure);
        }
        public int Delete(ImageOfProduct obj)
        {
            return connection.Execute($"DELETE FROM ImageOfProduct WHERE ProductId = {obj.ProductId} AND ColorId ='{obj.ColorId}' AND ImageUrl = '{obj.ImageUrl}'");
        }
    }
}
