using System;
using System.Threading.Tasks;
using Npgsql;

class Program
{
    static async Task Main()
    {
        string connStr = "Host=136.118.121.105;Port=5432;Database=zap_ecosystem_v200;Username=postgres;Password=Pg@Secret2026!;Include Error Detail=true;";
        await using var conn = new NpgsqlConnection(connStr);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand("SELECT column_name FROM information_schema.columns WHERE table_schema = 'people' AND table_name = 'customer'", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        Console.WriteLine("COLUMNS:");
        while (await reader.ReadAsync())
        {
            Console.WriteLine(reader.GetString(0));
        }
    }
}
