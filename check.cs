using System;
using Npgsql;

class Program {
    static void Main() {
        string connString = ""Host=136.118.121.105;Port=5432;Username=postgres;Password=1;Database=zap_ecosystem_final"";
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        using var cmd = new NpgsqlCommand(""SELECT table_schema, table_name FROM information_schema.tables WHERE table_name LIKE '%geo%';"", conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read()) {
            Console.WriteLine($""{reader[0]}.{reader[1]}"");
        }
    }
}
