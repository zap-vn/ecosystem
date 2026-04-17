const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function checkCatalog() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Catalog Schema Tables ---');
    const tables = await client.query("SELECT table_name FROM information_schema.tables WHERE table_schema = 'catalog'");
    console.log(tables.rows.map(x => x.table_name));

    await client.end();
}
checkCatalog();
