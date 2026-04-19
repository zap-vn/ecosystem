const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Columns of system.status_item_translation ---');
    const res = await client.query("SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'status_item_translation' AND table_schema = 'system'");
    console.log(JSON.stringify(res.rows, null, 2));

    await client.end();
}
check().catch(console.error);
