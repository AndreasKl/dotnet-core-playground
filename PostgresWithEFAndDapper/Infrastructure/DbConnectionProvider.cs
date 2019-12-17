using System.Data.Common;
using Npgsql;

namespace PostgresWithEFAndDapper.Infrastructure
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public DbConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection Connection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}