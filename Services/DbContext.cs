using System.Data;
using Npgsql;

namespace Services;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext()
    {
        _connectionString = 
            "Server=localhost; Port=5432; Database=dapper1_db; User Id=postgres; Password=Mm1311Scorpio$";

    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}