using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opc.Net.Demo
{
    public class SqlSugarHelper
    {
        private static string conn = $"Data Source={AppDomain.CurrentDomain.BaseDirectory}DB\\KopSoftSCADA.db;Version=3";

        public static SqlSugarClient DB
        {
            get => new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conn,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.SystemTable
            });
        }

        private static void Update()
        {
        }
    }
}