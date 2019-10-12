using Npgsql;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloDashboard.Data.ConnectionManager
{
    public class ConnectionManager
    {
        static NpgsqlConnection npSqlConnection = null;

        public static NpgsqlConnection GetMyNpgConnection()
        {
            string connectionString = "Server=172.16.10.6;Database=epace;User Id=epace_read;Password=epace";

            npSqlConnection = new NpgsqlConnection(connectionString);

            npSqlConnection.Open();

            return npSqlConnection;
        }

        public static void CloseNpgSqlConnection()
        {
            if (npSqlConnection != null)
            {
                npSqlConnection.Close();
            }
        }
    }
}
