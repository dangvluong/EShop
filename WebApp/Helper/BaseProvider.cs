using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;

namespace WebApp.Helper
{
    public class BaseProvider : IDisposable
    {
        IDbConnection connection;
        IConfiguration configuration;
        public BaseProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IDbConnection Connection
        {
            get
            {
                if(connection is null)
                {
                    connection = new SqlConnection(configuration.GetConnectionString("EzShop"));
                }
                return connection;
            }
        }

        public void Dispose()
        {
            if(connection != null)
            {
                connection.Dispose();
            }
        }
    }
}
