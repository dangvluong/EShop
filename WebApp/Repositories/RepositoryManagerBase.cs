using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Repositories
{
    public class RepositoryManagerBase : IDisposable
    {
        private IDbConnection connection;
        private IConfiguration configuration;
        public RepositoryManagerBase(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IDbConnection Connection
        {
            get
            {
                if(connection is null)
                {
                    connection = new SqlConnection(configuration.GetConnectionString("EShop"));
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
