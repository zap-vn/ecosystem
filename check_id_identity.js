const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Identity Details for platform.uom.id ---');
    const res = await client.query(`
        SELECT attname, attidentity 
        FROM pg_attribute 
        WHERE attrelid = 'platform.uom'::regclass AND attname = 'id'
    `);
    console.log(JSON.stringify(res.rows, null, 2));

    await client.end();
}
check().catch(console.error);
