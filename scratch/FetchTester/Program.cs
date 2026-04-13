using System;
using System.Data;
using Npgsql;

class P {
    static void Main() {
        try {
            var cb = new NpgsqlConnectionStringBuilder("Host=136.118.121.105;Port=5432;Database=ecosystem_v110;Username=postgres;Password=Pg@Secret2026!");
            using var c = new NpgsqlConnection(cb.ToString());
            c.Open();

            // 1. Check if there is any data in customer_membership
            using var cmdCount = c.CreateCommand();
            cmdCount.CommandText = "SELECT COUNT(*) FROM identity.customer_membership;";
            var count = Convert.ToInt32(cmdCount.ExecuteScalar());
            Console.WriteLine("Total memberships in DB: " + count);

            // 2. Fetch the first customer ID
            using var cmdGetCustomer = c.CreateCommand();
            cmdGetCustomer.CommandText = "SELECT id FROM identity.customer LIMIT 1;";
            var customerIdObj = cmdGetCustomer.ExecuteScalar();

            if (customerIdObj != null && customerIdObj != DBNull.Value)
            {
                var customerId = (Guid)customerIdObj;
                Console.WriteLine("Found customer ID: " + customerId);

                // 3. Insert mock membership if empty
                if (count == 0)
                {
                    using var cmdInsert = c.CreateCommand();
                    cmdInsert.CommandText = @"
                        INSERT INTO identity.customer_membership (id, customer_id, current_points, joined_at)
                        VALUES (@id, @custId, @points, @joinedAt);
                    ";
                    cmdInsert.Parameters.AddWithValue("id", Guid.NewGuid());
                    cmdInsert.Parameters.AddWithValue("custId", customerId);
                    cmdInsert.Parameters.AddWithValue("points", 150);
                    cmdInsert.Parameters.AddWithValue("joinedAt", DateTime.UtcNow.AddMonths(-1));
                    
                    cmdInsert.ExecuteNonQuery();
                    Console.WriteLine("Mock membership inserted for customer: " + customerId);
                }
            }
            else
            {
                Console.WriteLine("No customers found in database.");
            }

        } catch (Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
