using System;
using Npgsql;
using System.Threading.Tasks;

string connStr = "Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v110";
try {
    using var conn = new NpgsqlConnection(connStr);
    await conn.OpenAsync();
    
    // List all tables
    using var cmdTbl = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema='public' AND table_name LIKE '%location%';", conn);
    using var readerTbl = await cmdTbl.ExecuteReaderAsync();
    Console.WriteLine("Tables containing 'location':");
    while(await readerTbl.ReadAsync()) {
        Console.WriteLine(readerTbl.GetString(0));
    }
    await readerTbl.DisposeAsync();

    using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM locations", conn);
    var count = await cmd.ExecuteScalarAsync();
    Console.WriteLine("Table public.locations has " + count + " records.");
    
    using var cmd2 = new NpgsqlCommand("SELECT * FROM locations LIMIT 1", conn);
    using var reader = await cmd2.ExecuteReaderAsync();
    Console.WriteLine("Columns in locations:");
    for(int i=0; i<reader.FieldCount; i++) {
        Console.WriteLine("- " + reader.GetName(i));
    }

} catch(Exception ex) {
    Console.WriteLine("ERROR: " + ex.Message);
}
