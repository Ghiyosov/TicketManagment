using Npgsql;

namespace infrastructure.DataContext;

public class DapperContext
{
    readonly string _connectionString="Host=localhost;Port=5432;Database=ticketmanagmentdb;User Id=postgres;Password=832111;";

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}