const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function checkProduct() {
    const client = new Client({ connectionString });
    await client.connect();
    
    console.log('--- Product Table Columns ---');
    const cols = await client.query("SELECT column_name FROM information_schema.columns WHERE table_name = 'product' AND table_schema = 'catalog'");
    console.log(cols.rows.map(x => x.column_name));

    console.log('\n--- Product Sample ---');
    const res = await client.query("SELECT * FROM catalog.product LIMIT 1");
    console.log(res.rows[0]);

    if (res.rows[0]) {
        console.log('\n--- Product Count per Brand ---');
        const counts = await client.query("SELECT brand_id, count(*) FROM catalog.product GROUP BY brand_id LIMIT 5");
        console.log(counts.rows);
    }

    await client.end();
}
checkProduct();
