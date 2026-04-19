using Npgsql;
using System;
using BCrypt.Net;

class Program { 
    static void Main() { 
        using var conn = new NpgsqlConnection("Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v300");
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT email, password_hash FROM identity.user WHERE email = 'binh.hoang.coo@pho24.com.vn'", conn);
        using var reader = cmd.ExecuteReader();
        while(reader.Read()) {
            string hash = reader.GetString(1);
            Console.WriteLine("Hash : " + hash);
            bool isValid = BCrypt.Net.BCrypt.Verify("123456", hash);
            Console.WriteLine("Is 123456 valid? " + isValid);
        }
    }
}
