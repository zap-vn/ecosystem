const { Client } = require('pg');
const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function setupTestUser() {
    const client = new Client({ connectionString });
    await client.connect();

    // Set plain text password '123456' and status_id '2' (Active)
    const res = await client.query("UPDATE identity.user SET password_hash = '123456', status_id = 2 WHERE username = 'binh.hoang.coo'");
    
    if (res.rowCount > 0) {
        console.log('✅ Updated binh.hoang.coo with password 123456');
    } else {
        console.log('❌ User binh.hoang.coo not found');
    }

    await client.end();
}

setupTestUser();
