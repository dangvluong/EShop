﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductRepository
    {
        IConfiguration configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<Product> GetProducts(int index, int size, out int total)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Index", index, dbType: DbType.Int32);
                parameters.Add("@Size", size, dbType: DbType.Int32);
                parameters.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                IEnumerable<Product> products= connection.Query<Product>("GetProducts", parameters, commandType: CommandType.StoredProcedure);
                total = parameters.Get<int>("@Total");
                return products;
            }
        }
        public Product GetProductById(short id)
        {
            using(IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.QueryFirstOrDefault<Product>("GetProductById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
