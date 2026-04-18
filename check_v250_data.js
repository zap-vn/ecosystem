const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Data in platform.uom ---');
    const res = await client.query("SELECT id, name, code, is_active FROM platform.uom LIMIT 5");
    console.log(JSON.stringify(res.rows, null, 2));

    await client.end();
}
check().catch(console.error);
