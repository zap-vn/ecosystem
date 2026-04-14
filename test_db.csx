using System;
using System.Threading.Tasks;
using Npgsql;

// The connection string is from Identity Program.cs
string connStr = "Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v110";

try {
    using var conn = new NpgsqlConnection(connStr);
    await conn.OpenAsync();
    
    // Check tables
    using var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema='public' AND table_name LIKE 'location%';", conn);
    using var reader = await cmd.ExecuteReaderAsync();
    Console.WriteLine("Tables matching 'location*':");
    while(await reader.ReadAsync()) {
        Console.WriteLine(reader.GetString(0));
    }
}
catch (Exception e) {
    Console.WriteLine(e.ToString());
}
