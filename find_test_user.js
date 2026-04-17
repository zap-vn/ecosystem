const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function findTestUser() {
    const client = new Client({ connectionString });
    await client.connect();
    
    // Find users with non-bcrypt hashes
    const res = await client.query("SELECT username, email, password_hash FROM identity.user WHERE password_hash NOT LIKE '$2%' LIMIT 10");
    console.log('Legacy Users:', res.rows);

    // Search for common test users
    const res2 = await client.query("SELECT username, password_hash FROM identity.user WHERE username IN ('admin', 'test', 'demo', 'zapadmin')");
    console.log('Common Test Users:', res2.rows);

    await client.end();
}
findTestUser();
