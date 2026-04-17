const fs = require('fs');

const gateway = 'http://localhost:5120';

const collection = {
  info: {
    name: 'ZAP Ecosystem V1 - Standardized Final',
    description: 'ZAP ERP Ecosystem - Full API Test Suite\n\nStandards:\n- Version: v1\n- Case: snake_case (URLs, Body, Response)\n- Gateway: http://localhost:5120',
    schema: 'https://schema.getpostman.com/json/collection/v2.1.0/collection.json',
    _postman_id: 'zap-ecosystem-final-v1'
  },
  variable: [
    { key: 'gateway', value: gateway, type: 'string' }
  ],
  item: [
    {
      name: '01. Identity & Auth',
      item: [
        {
          name: 'App Auth - Login Customer',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/auth/customer/login', host: ['{{gateway}}'], path: ['api', 'v1', 'auth', 'customer', 'login'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({
                phone_number: '0901234567',
                password: 'password123'
              }, null, 2)
            }
          }
        },
        {
          name: 'Login - Check Account',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/auth/login/check_account', host: ['{{gateway}}'], path: ['api', 'v1', 'auth', 'login', 'check_account'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({
                account_identifier: 'test@zap.vn'
              }, null, 2)
            }
          }
        },
        {
          name: 'Login - Send OTP',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/auth/login/send_otp', host: ['{{gateway}}'], path: ['api', 'v1', 'auth', 'login', 'send_otp'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({
                phone_number: '0901234567'
              }, null, 2)
            }
          }
        }
      ]
    },
    {
      name: '02. Catalog',
      item: [
        {
          name: 'Geo Countries - List',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/catalog/geo_countries/list', host: ['{{gateway}}'], path: ['api', 'v1', 'catalog', 'geo_countries', 'list'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({ page_index: 1, page_size: 10, search: '', is_active: true }, null, 2)
            }
          }
        },
        {
          name: 'Menus - List',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/catalog/menus/list', host: ['{{gateway}}'], path: ['api', 'v1', 'catalog', 'menus', 'list'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({ page_index: 1, page_size: 10, search: '' }, null, 2)
            }
          }
        },
        {
          name: 'Products - List',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/catalog/products/list', host: ['{{gateway}}'], path: ['api', 'v1', 'catalog', 'products', 'list'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({
                page_index: 1,
                page_size: 10,
                search: '',
                filters: { category_id: null, status_id: 1, location_id: null },
                sort: { field: "name", descending: false }
              }, null, 2)
            }
          }
        }
      ]
    },
    {
      name: '03. CRM',
      item: [
        {
          name: 'Customers - List',
          request: {
            method: 'GET',
            url: { 
                raw: '{{gateway}}/api/v1/crm/customers?page_index=1&page_size=10', 
                host: ['{{gateway}}'], 
                path: ['api', 'v1', 'crm', 'customers'],
                query: [{ key: 'page_index', value: '1' }, { key: 'page_size', value: '10' }]
            }
          }
        },
        {
            name: 'Promotions - List',
            request: {
                method: 'POST',
                header: [{ key: 'Content-Type', value: 'application/json' }],
                url: { raw: '{{gateway}}/api/v1/crm/promotions/list', host: ['{{gateway}}'], path: ['api', 'v1', 'crm', 'promotions', 'list'] },
                body: {
                    mode: 'raw',
                    raw: JSON.stringify({ page_index: 1, page_size: 10, search: "" }, null, 2)
                }
            }
        }
      ]
    },
    {
      name: '04. Sales',
      item: [
        {
          name: 'Orders - List',
          request: {
            method: 'POST',
            header: [{ key: 'Content-Type', value: 'application/json' }],
            url: { raw: '{{gateway}}/api/v1/sales/orders/list', host: ['{{gateway}}'], path: ['api', 'v1', 'sales', 'orders', 'list'] },
            body: {
              mode: 'raw',
              raw: JSON.stringify({
                page_index: 1,
                page_size: 10,
                search: '',
                status_id: null
              }, null, 2)
            }
          }
        },
        {
          name: 'Dining Options - List',
          request: {
            method: 'GET',
            url: { raw: '{{gateway}}/api/v1/sales/dining_options', host: ['{{gateway}}'], path: ['api', 'v1', 'sales', 'dining_options'] }
          }
        }
      ]
    },
    {
        name: '99. System Health (Ping)',
        item: [
            { name: 'CRM Ping', request: { method: 'GET', url: '{{gateway}}/api/v1/crm/ping' } },
            { name: 'Sales Ping', request: { method: 'GET', url: '{{gateway}}/api/v1/sales/ping' } },
            { name: 'HRM Ping', request: { method: 'GET', url: '{{gateway}}/api/v1/hrm/ping' } },
            { name: 'Inventory Ping', request: { method: 'GET', url: '{{gateway}}/api/v1/inventory/ping' } },
            { name: 'Finance Ping', request: { method: 'GET', url: '{{gateway}}/api/v1/finance/ping' } }
        ]
    }
  ]
};

const jsonResult = JSON.stringify(collection, null, 2);

// Standardizing ALL potential Postman files in the workspace root
const filesToUpdate = [
  'ZAP_Ecosystem_V2_Postman.json',
  'ZAP_Stabilized_Postman.json',
  'ZAP_Ecosystem_Postman_Collection.json',
  'ZAP_Ecosystem_API.json'
];

filesToUpdate.forEach(file => {
  fs.writeFileSync(file, jsonResult);
  console.log(`Updated: ${file}`);
});
