using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BaseRepository
    {
        //protected string connectionString;
        protected IDbConnection connection;
        public BaseRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
    }
}
