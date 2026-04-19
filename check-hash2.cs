using Npgsql;
using System;
class Program { 
    static void Main() { 
        using var conn = new NpgsqlConnection(\"Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v300\");
        conn.Open();
        using var cmd = new NpgsqlCommand(\"SELECT id, username, email, password_hash, LENGTH(password_hash) FROM identity.user WHERE email LIKE '%pho24.com.vn'\", conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read()) {
            Console.WriteLine(reader.GetGuid(0) + \" | \" + reader.GetString(1) + \" | \" + reader.GetString(2) + \" | \" + reader.GetString(3) + \" | len: \" + reader.GetInt32(4));
        }
    }
}
