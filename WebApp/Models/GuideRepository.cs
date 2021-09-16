using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models
{
    public class GuideRepository : BaseRepository
    {
        //IConfiguration configuration;
        public GuideRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<Guide> GetGuids()
        {
            return connection.Query<Guide>("GetGuides", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Guide> GetGuidesByProduct(short productId)
        {
            return connection.Query<Guide>("GetGuidesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
        }
        public int Edit(Guide obj)
        {
            return connection.Execute($"UPDATE Guide SET GuideDescription = '{obj.GuideDescription}' WHERE GuideId = {obj.GuideId}");
        }
        public int Delete(short id)
        {
            return connection.Execute($"UPDATE Guide SET IsDeleted = 1 WHERE GuideId = {id}");
        }
        public int Add(Guide obj)
        {
            return connection.Execute($"INSERT INTO Guide(GuideDescription) VALUES('{obj.GuideDescription}')");
        }
    }
}
