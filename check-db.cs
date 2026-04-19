using Npgsql;
using System;
class Program { 
    static void Main() { 
        using var conn = new NpgsqlConnection("Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v200");
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT column_name FROM information_schema.columns WHERE table_schema = 'identity' AND table_name = 'user'", conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read()) Console.WriteLine(reader.GetString(0));
    }
}
