using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Configuration;
using BlockBrawl.Objects;

namespace BlockBrawl.GameHandlerObjects
{
    class HighScore
    {
        string connectionString;
        List<DataRead> dataReads;
        NpgsqlConnection connection;
        public HighScore()
        {
            connectionString = ConfigurationManager.ConnectionStrings["visualstudio"].ConnectionString;
            PullFromDB();
        }
        public List<DataRead> PullFromDB()
        {
            using (connection = new NpgsqlConnection(connectionString))
            {
                var output = connection.Query<DataRead>("select * from records order by id asc;").ToList();
                dataReads = output;
            }
            return dataReads;
        }
    }
}
