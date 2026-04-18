const { Client } = require('pg');
const fs = require('fs');
const path = require('path');

const clientConfig = {
    host: '136.118.121.105',
    port: 5432,
    database: 'zap_ecosystem_v250',
    user: 'postgres',
    password: 'Pg@Secret2026!'
};

async function run() {
    const client = new Client(clientConfig);
    try {
        await client.connect();
        console.log('✅ Connected to zap_ecosystem_v250 directly');

        const cols2 = await client.query(`SELECT column_name FROM information_schema.columns WHERE table_schema = 'system' AND table_name = 'entity_dictionary'`);
        console.log("Existing columns in system.entity_dictionary:", cols2.rows.map(r => r.column_name).join(", "));
        
        process.exit(0);
        
        // 1. Create Dictionary Schema and Tables directly
        const sqlTablesRaw = fs.readFileSync(path.join(__dirname, 'CREATE_DICTIONARY_TABLES.sql'), 'utf8');
        const statements1 = sqlTablesRaw.split(';').filter(s => s.trim().length > 5);
        for (const s of statements1) {
            try {
                await client.query(s);
            } catch (err) {
                console.error("❌ Failed SQL:", s);
                throw err;
            }
        }
        console.log('✅ Dictionary Tables and System Schema setup successfully.');

        // 2. Insert Data directly
        const sqlDataRaw = fs.readFileSync(path.join(__dirname, 'MOCK_DATA_SETUP.sql'), 'utf8');
        const statements2 = sqlDataRaw.split(';').filter(s => s.trim().length > 5);
        for (const s of statements2) {
            try {
                await client.query(s);
            } catch (err) {
                console.error("❌ Failed SQL:", s);
                throw err;
            }
        }
        console.log('✅ Mock Data for catalog.unit and identity.user configured successfully.');

        console.log('✅ Database preparation complete.');
    } catch (err) {
        console.error('❌ Database error:', err);
    } finally {
        await client.end();
    }
}

run();
