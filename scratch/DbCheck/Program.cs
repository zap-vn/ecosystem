using System;
using System.Data;
using Npgsql;

class Program
{
    static void Main()
    {
        try {
            using (var conn = new NpgsqlConnection("Host=136.118.121.105;Port=5432;Database=zap_ecosystem_v200;Username=postgres;Password=Pg@Secret2026!;"))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT id, email, phone_number, password_hash, full_name FROM identity.users LIMIT 5;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["full_name"]} | Email: {reader["email"]} | Phone: {reader["phone_number"]} | Hash: {reader["password_hash"]}");
                    }
                }
            }
        } catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
