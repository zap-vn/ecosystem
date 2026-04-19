const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/postgres";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Databases ---');
    const res = await client.query("SELECT datname FROM pg_database WHERE datistemplate = false");
    console.log(JSON.stringify(res.rows, null, 2));

    await client.end();
}
check().catch(console.error);
