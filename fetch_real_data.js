const { Client } = require('pg');
const fs = require('fs');

const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v250";

async function fetchRealData() {
    const client = new Client({ connectionString });
    try {
        await client.connect();
        const data = {};

        // 1. Get IDs from Product table
        const prodData = await client.query('SELECT id, tenant_id, brand_id, category_id, product_code FROM catalog.product WHERE tenant_id IS NOT NULL LIMIT 1');
        const p = prodData.rows[0];
        
        data.tenant_id = p?.tenant_id || 'f47ac10b-fa24-4372-a567-6e8c00000000';
        data.brand_id = p?.brand_id || 'f47ac10b-fa24-4372-a567-00c100000001';
        data.product_id = p?.id || 'e6d1f120-667d-420e-a9bd-bbab5e40432a';
        data.category_id = p?.category_id || 'ee42f135-b6a8-40a7-9b69-758cbf8b1a8f';

        // 2. User & Customer
        const userRes = await client.query('SELECT id, username, email FROM identity.user WHERE status_id = 9001 LIMIT 1');
        data.user_id = userRes.rows[0]?.id || 'f47ac10b-fa24-4372-a567-222271ae0001';
        data.username = userRes.rows[0]?.username || 'binh.hoang.coo';
        data.email = userRes.rows[0]?.email || 'binh.hoang.coo@zap.vn';
        
        const custRes = await client.query('SELECT id, phone_number FROM people.customer LIMIT 1');
        data.customer_id = custRes.rows[0]?.id || data.user_id;
        data.phone_number = custRes.rows[0]?.phone_number || '0903079341';

        fs.writeFileSync('REAL_DATA_SAMPLE.json', JSON.stringify(data, null, 2));
        console.log('✅ Updated REAL_DATA_SAMPLE.json with database IDs.');
        console.log(data);

    } catch (err) {
        console.error('❌ Database error:', err);
    } finally {
        await client.end();
    }
}

fetchRealData();
