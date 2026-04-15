
using System;
using Npgsql;
var connString = "Host=136.118.121.105;Port=5432;Database=zap_ecosystem_v200;Username=postgres;Password=Pg@Secret2026!;Include Error Detail=true;";
using var conn = new NpgsqlConnection(connString);
conn.Open();
using var cmd = new NpgsqlCommand(@"
    SELECT column_name, data_type 
    FROM information_schema.columns 
    WHERE table_schema = 'identity' AND table_name = 'user';", conn);
using var reader = cmd.ExecuteReader();
while (reader.Read()) {
    Console.WriteLine($"{reader.GetString(0)}: {reader.GetString(1)}");
}

