const fs = require('fs');
const path = require('path');
const https = require('https');

const rootDir = 'src';
const API_KEY = 'PMAK-69e22f257e44dc0001c11bbd-8add08c0a15d437deba43d3267b8be7a09';
const COLLECTION_UID = '15026203-0a7f9d08-edc2-4296-820a-2f774180de5a';

const endpoints = [];

function toSnakeCase(str) {
    return str.replace(/[A-Z]/g, letter => `_${letter.toLowerCase()}`).replace(/^_/, '');
}

// Map of common names to sample values
const sampleValues = {
    'string': 'string',
    'int': 0,
    'long': 0,
    'double': 0.0,
    'decimal': 0.0,
    'bool': true,
    'boolean': true,
    'guid': '00000000-0000-0000-0000-000000000000',
    'datetime': new Date().toISOString()
};

function getSampleForClass(className) {
    // Try to find the file for this class
    const searchFiles = [];
    const walk = (d) => {
        fs.readdirSync(d).forEach(f => {
            const p = path.join(d, f);
            if (fs.statSync(p).isDirectory()) walk(p);
            else if (f === `${className}.cs`) searchFiles.push(p);
        });
    };
    walk('src');

    if (searchFiles.length === 0) return {};

    const content = fs.readFileSync(searchFiles[0], 'utf8');
    const sample = {};
    const lines = content.split('\n');
    
    lines.forEach(line => {
        const propMatch = line.match(/public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*set;\s*}/);
        if (propMatch) {
            const type = propMatch[1].toLowerCase();
            const name = toSnakeCase(propMatch[2]);
            
            if (type.includes('string')) sample[name] = 'string';
            else if (type.includes('int') || type.includes('long')) sample[name] = 0;
            else if (type.includes('bool')) sample[name] = true;
            else if (type.includes('guid')) sample[name] = '00000000-0000-0000-0000-000000000000';
            else if (type.includes('datetime')) sample[name] = new Date().toISOString();
            else sample[name] = null;
        }
    });
    return sample;
}

function scanDir(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
        const fullPath = path.join(dir, file);
        if (fs.statSync(fullPath).isDirectory()) {
            scanDir(fullPath);
        } else if (file.endsWith('Controller.cs')) {
            parseController(fullPath);
        }
    }
}

function parseController(filePath) {
    const content = fs.readFileSync(filePath, 'utf8');
    const routeMatch = content.match(/\[Route\("(.*?)"\)\]/);
    const baseRoute = routeMatch ? routeMatch[1] : '';
    
    const routeParts = baseRoute.split('/');
    const moduleName = routeParts[2] || 'System';
    const resourceName = routeParts[3] || path.basename(filePath).replace('Controller.cs', '');

    const lines = content.split('\n');
    let currentAction = null;

    lines.forEach(line => {
        const httpMatch = line.match(/\[Http(Get|Post|Put|Delete)(?:\("(.*?)"\))?\]/);
        if (httpMatch) {
            currentAction = {
                method: httpMatch[1].toUpperCase(),
                subRoute: httpMatch[2] || '',
                baseRoute: baseRoute,
                module: moduleName,
                resource: resourceName,
                fullRoute: (baseRoute + '/' + (httpMatch[2] || '')).replace(/\/+/g, '/').replace(/\/$/, '')
            };
        } else if (currentAction && line.includes('public async Task<IActionResult>')) {
            const nameMatch = line.match(/public async Task<IActionResult>\s+(\w+)\s*\((.*?)\)/);
            if (nameMatch) {
                currentAction.name = nameMatch[1];
                const paramContent = nameMatch[2];
                // Try to extract the DTO class name
                const dtoMatch = paramContent.match(/\[FromBody\]\s*([\w]+)\s*/);
                if (dtoMatch) {
                    currentAction.dtoClass = dtoMatch[1];
                }
                endpoints.push(currentAction);
                currentAction = null;
            }
        }
    });
}

scanDir(rootDir);

// Build Hierarchical Folder Structure
const folders = {};

endpoints.forEach(e => {
    const mod = e.module.toUpperCase();
    const res = e.resource.charAt(0).toUpperCase() + e.resource.slice(1);
    
    if (!folders[mod]) folders[mod] = {};
    if (!folders[mod][res]) folders[mod][res] = [];
    
    let bodySample = e.dtoClass ? getSampleForClass(e.dtoClass) : {};
    
    // Hardcoded logic for known important ones (Base values)
    if (e.name === 'LoginWithPassword' || e.name === 'VerifyOtpAndLogin' || e.name === 'SendOtp') {
        bodySample.account = 'binh.hoang.coo@pho24.com.vn'; // The system expects Email or Phone
        bodySample.password = '123456';
        bodySample.otp_code = '123456';
        if (bodySample.phone_number) delete bodySample.phone_number;
    }

    // Inject Real Data (Final override)
    if (fs.existsSync('REAL_DATA_SAMPLE.json')) {
        const real = JSON.parse(fs.readFileSync('REAL_DATA_SAMPLE.json', 'utf8'));
        Object.keys(bodySample).forEach(key => {
            if (key.includes('tenant_id')) bodySample[key] = real.tenant_id;
            if (key.includes('brand_id')) bodySample[key] = real.brand_id;
            if (key.includes('category_id')) bodySample[key] = real.category_id;
            if (key.includes('product_id')) bodySample[key] = real.product_id;
            if (key.includes('user_id')) bodySample[key] = real.user_id;
            if (key.includes('customer_id')) bodySample[key] = real.customer_id;
            if (key.includes('phone_number')) bodySample[key] = real.phone_number;
            if (key.includes('page_size')) bodySample[key] = 10;
            if (key.includes('page_index') || key.includes('page_number') || key === 'page') bodySample[key] = 1;
            if (key === 'search' || key === 'field') bodySample[key] = '';
            if (key === 'filters' || key === 'sort') bodySample[key] = {};
            
            // Map username or email to account for Admin Login
            if (key === 'account' && real.username) bodySample[key] = real.username;
            else if (key === 'account' && real.email) bodySample[key] = real.email;
            else if (key === 'account' && real.phone_number) bodySample[key] = real.phone_number;
        });
    }

    folders[mod][res].push({ ...e, bodySample });
});

// Convert to Postman JSON
function buildPostmanItems() {
    return Object.entries(folders).sort().map(([mod, resources]) => ({
        name: mod,
        item: Object.entries(resources).sort().map(([res, actions]) => ({
            name: res,
            item: actions.map(a => {
                const item = {
                    name: a.name,
                    request: {
                        method: a.method,
                        header: [
                            { key: 'Content-Type', value: 'application/json', type: 'text' },
                            { key: 'Accept-Language', value: 'vi-VN, vi;q=0.9, en-US;q=0.8, en;q=0.7', type: 'text', description: '2 for VI, 1 for EN' }
                        ],
                        url: {
                            raw: `{{gateway}}/${a.fullRoute}`,
                            host: ['{{gateway}}'],
                            path: a.fullRoute.split('/')
                        },
                        body: a.method === 'POST' || a.method === 'PUT' ? {
                            mode: 'raw',
                            raw: JSON.stringify(a.bodySample || {}, null, 2)
                        } : undefined
                    }
                };

                // Add Automation Script for Login
                if (a.name.toLowerCase().includes('login') || a.name === 'VerifyOtpAndLogin') {
                    item.event = [{
                        listen: "test",
                        script: {
                            exec: [
                                "if (pm.response.code === 200) {",
                                "    var jsonData = pm.response.json();",
                                "    if (jsonData.data && jsonData.data.token) {",
                                "        pm.collectionVariables.set('token', jsonData.data.token);",
                                "        console.log('✅ Token Captured Successfully');",
                                "    }",
                                "}"
                            ],
                            type: "text/javascript"
                        }
                    }];
                }
                return item;
            })
        }))
    }));
}

const collection = {
    info: {
        name: 'ZAP Ecosystem - Master Suite v2',
        description: 'Updated with Automatic Token Capture and Bearer Auth.',
        schema: 'https://schema.getpostman.com/json/collection/v2.1.0/collection.json'
    },
    variable: [
        { key: 'gateway', value: 'http://localhost:5120', type: 'string' },
        { key: 'token', value: '', type: 'string' }
    ],
    auth: {
        type: 'bearer',
        bearer: [
            { key: 'token', value: '{{token}}', type: 'string' }
        ]
    },
    item: buildPostmanItems()
};

fs.writeFileSync('ZAP_Professional_Postman.json', JSON.stringify(collection, null, 2));

// Cloud Sync
console.log(`Syncing to Postman Cloud (UID: ${COLLECTION_UID})...`);
const options = {
    hostname: 'api.getpostman.com',
    path: `/collections/${COLLECTION_UID}`,
    method: 'PUT',
    headers: {
        'X-Api-Key': API_KEY,
        'Content-Type': 'application/json'
    }
};

const req = https.request(options, (res) => {
    let b = '';
    res.on('data', d => b += d);
    res.on('end', () => {
        const result = JSON.parse(b);
        if (result.collection) console.log('✅ MASTER COLLECTION UPDATED WITH BODIES!');
        else console.error('❌ FAILED:', result);
    });
});
req.write(JSON.stringify({ collection }));
req.end();
