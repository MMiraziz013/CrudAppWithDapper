using System.Data;
using Npgsql;

namespace Services;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext()
    {
        string _connectionString =
         "YOUR CONNECTION STRING";

    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
