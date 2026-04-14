using System;
using System.Threading.Tasks;
using Npgsql;

class Program
{
    static async Task Main()
    {
        string connStr = "Host=136.118.121.105;Port=5432;Database=postgres;Username=postgres;Password=Pg@Secret2026!;Include Error Detail=true;";
        await using var conn = new NpgsqlConnection(connStr);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT datname FROM pg_database WHERE datistemplate = false", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        Console.WriteLine("DATABASES:");
        while (await reader.ReadAsync())
        {
            string db = reader.GetString(0);
            if (db == "postgres") continue;

            Console.WriteLine($"DB: {db}");
            try
            {
                var conn2Str = connStr.Replace("Database=postgres", $"Database={db}");
                await using var conn2 = new NpgsqlConnection(conn2Str);
                await conn2.OpenAsync();
                await using var cmd2 = new NpgsqlCommand("SELECT string_agg(table_schema || '.' || table_name, ', ') FROM information_schema.tables WHERE table_schema NOT IN ('pg_catalog', 'information_schema')", conn2);
                var tables = await cmd2.ExecuteScalarAsync();
                Console.WriteLine($"   -> TABLES: {tables}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   -> ERR: {ex.Message}");
            }
        }
    }
}
