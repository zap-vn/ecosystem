const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Sequences in platform ---');
    const res = await client.query(`
        SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = 'platform'
    `);
    console.log(JSON.stringify(res.rows, null, 2));

    await client.end();
}
check().catch(console.error);
