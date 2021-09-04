using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BaseRepository
    {
        protected string connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EzShop");
        }
    }
}
