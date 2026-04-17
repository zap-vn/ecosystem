const { Client } = require('pg');
const fs = require('fs');

const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function findTables() {
    const client = new Client({ connectionString });
    try {
        await client.connect();
        
        // Find tenant related
        const resTenant = await client.query("SELECT schemaname, tablename FROM pg_catalog.pg_tables WHERE tablename LIKE '%tenant%'");
        console.log('Tenant tables:', resTenant.rows);

        // Find brand related
        const resBrand = await client.query("SELECT schemaname, tablename FROM pg_catalog.pg_tables WHERE tablename LIKE '%brand%'");
        console.log('Brand tables:', resBrand.rows);

        // Find product related
        const resProd = await client.query("SELECT schemaname, tablename FROM pg_catalog.pg_tables WHERE tablename LIKE '%product%'");
        console.log('Product tables:', resProd.rows);

    } catch (err) {
        console.error(err);
    } finally {
        await client.end();
    }
}

findTables();
