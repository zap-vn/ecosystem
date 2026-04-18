const fs = require('fs');
const https = require('https');

const API_KEY = process.env.POSTMAN_API_KEY;
if (!API_KEY) {
    console.error('Missing POSTMAN_API_KEY');
    process.exit(1);
}

// 1. Sync Collection
try {
    const rawCol = fs.readFileSync('ZAP_Ecosystem_Units.postman_collection.json', 'utf8');
    const collectionData = JSON.parse(rawCol);
    const colId = collectionData.info._postman_id;
    
    // Wrap in "collection"
    const payloadBuffer = Buffer.from(JSON.stringify({ collection: collectionData }), 'utf8');
    
    // Try PUT to update
    const colOptions = {
        hostname: 'api.getpostman.com',
        path: `/collections/${colId}`,
        method: 'PUT',
        headers: {
            'X-Api-Key': API_KEY,
            'Content-Type': 'application/json',
            'Content-Length': payloadBuffer.length
        }
    };
    
    const req = https.request(colOptions, (res) => {
        let chunks = [];
        res.on('data', d => chunks.push(d));
        res.on('end', () => {
            const result = Buffer.concat(chunks).toString();
            console.log(`[Collection Sync] Status: ${res.statusCode} | Response:`, result);
        });
    });
    
    req.write(payloadBuffer);
    req.end();
} catch (e) {
    console.error("Collection Deploy error:", e.message);
}

// 2. Sync Environment
try {
    const rawEnv = fs.readFileSync('ZAP_GCP_Environment.postman_environment.json', 'utf8');
    const envData = JSON.parse(rawEnv);
    const envId = envData.id;
    
    // Wrap in "environment"
    const envPayloadBuffer = Buffer.from(JSON.stringify({ environment: envData }), 'utf8');
    
    const envOptions = {
        hostname: 'api.getpostman.com',
        path: `/environments/${envId}`,
        method: 'PUT',
        headers: {
            'X-Api-Key': API_KEY,
            'Content-Type': 'application/json',
            'Content-Length': envPayloadBuffer.length
        }
    };
    
    const reqEnv = https.request(envOptions, (res) => {
        let chunks = [];
        res.on('data', d => chunks.push(d));
        res.on('end', () => {
             const result = Buffer.concat(chunks).toString();
             // If PUT fails because it doesn't exist, we fallback to POST
             if (res.statusCode === 404 || res.statusCode === 400) {
                 console.log("[Environment Sync] Environment not found, attempting POST to create new...");
                 const postOptions = { ...envOptions, path: '/environments', method: 'POST' };
                 const postReq = https.request(postOptions, r => {
                     let pChunks = [];
                     r.on('data', d => pChunks.push(d));
                     r.on('end', () => console.log(`[Environment Sync] (POST) Status: ${r.statusCode} | Response:`, Buffer.concat(pChunks).toString()));
                 });
                 postReq.write(envPayloadBuffer);
                 postReq.end();
             } else {
                 console.log(`[Environment Sync] Status: ${res.statusCode} | Response:`, result);
             }
        });
    });
    
    reqEnv.write(envPayloadBuffer);
    reqEnv.end();
} catch (e) {
    console.error("Environment Deploy error:", e.message);
}
