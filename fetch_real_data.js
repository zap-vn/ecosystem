const { Client } = require('pg');
const fs = require('fs');

const connectionString = "postgresql://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200";

async function fetchRealData() {
    const client = new Client({ connectionString });
    try {
        await client.connect();
        const data = {};

        // 1. Fetch Tenant
        const tenantRes = await client.query('SELECT id FROM identity.tenant_node LIMIT 1');
        data.tenant_id = tenantRes.rows[0]?.id || '00000000-0000-0000-0000-000000000000';

        // 2. Fetch Brand
        const brandRes = await client.query('SELECT id FROM catalog.brand LIMIT 1');
        data.brand_id = brandRes.rows[0]?.id || '00000000-0000-0000-0000-000000000000';

        // 3. Fetch Category
        const catRes = await client.query('SELECT id FROM platform.category_item LIMIT 1');
        data.category_id = catRes.rows[0]?.id || '00000000-0000-0000-0000-000000000000';

        // 4. Fetch Product
        const prodRes = await client.query('SELECT id FROM catalog.product LIMIT 1');
        data.product_id = prodRes.rows[0]?.id || '00000000-0000-0000-0000-000000000000';

        // 5. Fetch User
        const userRes = await client.query('SELECT id FROM identity.user LIMIT 1');
        data.user_id = userRes.rows[0]?.id || '00000000-0000-0000-0000-000000000000';
        
        // 6. Fetch Customer (for phone number)
        const custRes = await client.query('SELECT id, phone_number FROM identity.customers LIMIT 1');
        data.customer_id = custRes.rows[0]?.id || data.user_id;
        data.phone_number = custRes.rows[0]?.phone_number || '0901234567';

        fs.writeFileSync('REAL_DATA_SAMPLE.json', JSON.stringify(data, null, 2));
        console.log('✅ Updated REAL_DATA_SAMPLE.json with database IDs.');

    } catch (err) {
        console.error('❌ Database error:', err);
    } finally {
        await client.end();
    }
}

fetchRealData();
