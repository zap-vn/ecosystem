const https = require('https');

const API_KEY = 'PMAK-69e22f257e44dc0001c11bbd-8add08c0a15d437deba43d3267b8be7a09';
const WORKSPACE_ID = '432e9c04-a50a-48f6-8e45-7ab4d5cb1524';

const environment = {
    name: 'ZAP - Production/Staging',
    values: [
        { key: 'gateway', value: 'http://localhost:5120', enabled: true },
        { key: 'api_key', value: API_KEY, enabled: true }
    ]
};

async function deployEnvironment() {
    console.log('Deploying environment to Postman Cloud...');
    
    const options = {
        hostname: 'api.getpostman.com',
        path: `/environments?workspace=${WORKSPACE_ID}`,
        method: 'POST',
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
            if (result.environment) console.log('✅ ENVIRONMENT DEPLOYED SUCCESSFULLY!');
            else console.error('❌ FAILED:', result);
        });
    });

    req.write(JSON.stringify({ environment }));
    req.end();
}

deployEnvironment();
