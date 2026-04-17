const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function check() {
    const client = new Client({ connectionString });
    await client.connect();
    
    const resColumns = await client.query("SELECT column_name FROM information_schema.columns WHERE table_schema = 'identity' AND table_name = 'user'");
    console.log('User columns:', resColumns.rows.map(x => x.column_name));

    const resData = await client.query("SELECT * FROM identity.user LIMIT 1");
    console.log('User sample:', resData.rows[0]);

    await client.end();
}
check();
