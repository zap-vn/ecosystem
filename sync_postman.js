const fs = require('fs');
const https = require('https');

const API_KEY = process.env.POSTMAN_API_KEY;
if (!API_KEY) {
    console.error('Missing POSTMAN_API_KEY');
    process.exit(1);
}

// Helper for HTTP requests
function makeRequest(options, payloadBuffer = null) {
    return new Promise((resolve, reject) => {
        const req = https.request(options, (res) => {
            let chunks = [];
            res.on('data', d => chunks.push(d));
            res.on('end', () => {
                const resultStr = Buffer.concat(chunks).toString();
                resolve({ statusCode: res.statusCode, body: resultStr });
            });
        });
        req.on('error', reject);
        if (payloadBuffer) req.write(payloadBuffer);
        req.end();
    });
}

const TARGET_WORKSPACE_NAME = "ecosystem";

async function run() {
    try {
        console.log("Fetching Postman Workspaces...");
        const wsRes = await makeRequest({
            hostname: 'api.getpostman.com',
            path: '/workspaces',
            method: 'GET',
            headers: { 'X-Api-Key': API_KEY }
        });

        if (wsRes.statusCode !== 200) {
            throw new Error("Failed to fetch workspaces. Check your API Key.");
        }

        const workspaces = JSON.parse(wsRes.body).workspaces;
        const targetWs = workspaces.find(w => w.name.toLowerCase() === TARGET_WORKSPACE_NAME.toLowerCase());
        
        let workspaceQuery = "";
        if (targetWs) {
            console.log(`Found Target Workspace: "${targetWs.name}" (ID: ${targetWs.id})`);
            workspaceQuery = `?workspace=${targetWs.id}`;
        } else {
            console.log(`Workspace "${TARGET_WORKSPACE_NAME}" not found. Falling back to default Personal Workspace.`);
        }

        // 1. Sync Collection
        try {
            const rawCol = fs.readFileSync('ZAP_Ecosystem_Units.postman_collection.json', 'utf8');
            const collectionData = JSON.parse(rawCol);
            const colId = collectionData.info._postman_id;
            const payloadBuffer = Buffer.from(JSON.stringify({ collection: collectionData }), 'utf8');
            
            console.log(`[Collection] Attempting to PUT /collections/${colId}`);
            let res = await makeRequest({
                hostname: 'api.getpostman.com',
                path: `/collections/${colId}`,
                method: 'PUT',
                headers: { 'X-Api-Key': API_KEY, 'Content-Type': 'application/json', 'Content-Length': payloadBuffer.length }
            }, payloadBuffer);

            if (res.statusCode === 404 || res.statusCode === 400 || res.statusCode === 403) {
                console.log(`[Collection] PUT failed (${res.statusCode}), reverting to POST (Create new)...`);
                res = await makeRequest({
                    hostname: 'api.getpostman.com',
                    path: `/collections${workspaceQuery}`,
                    method: 'POST',
                    headers: { 'X-Api-Key': API_KEY, 'Content-Type': 'application/json', 'Content-Length': payloadBuffer.length }
                }, payloadBuffer);
            }
            console.log(`[Collection Sync] Status: ${res.statusCode}`);
        } catch (e) {
            console.error("Collection Deploy error:", e.message);
        }

        // 2. Sync Environment
        try {
            const rawEnv = fs.readFileSync('ZAP_GCP_Environment.postman_environment.json', 'utf8');
            const envData = JSON.parse(rawEnv);
            const envId = envData.id;
            const envPayloadBuffer = Buffer.from(JSON.stringify({ environment: envData }), 'utf8');
            
            console.log(`[Environment] Attempting to PUT /environments/${envId}`);
            let resEnv = await makeRequest({
                hostname: 'api.getpostman.com',
                path: `/environments/${envId}`,
                method: 'PUT',
                headers: { 'X-Api-Key': API_KEY, 'Content-Type': 'application/json', 'Content-Length': envPayloadBuffer.length }
            }, envPayloadBuffer);

            if (resEnv.statusCode === 404 || resEnv.statusCode === 400 || resEnv.statusCode === 403) {
                console.log(`[Environment] PUT failed (${resEnv.statusCode}), reverting to POST (Create new)...`);
                resEnv = await makeRequest({
                    hostname: 'api.getpostman.com',
                    path: `/environments${workspaceQuery}`,
                    method: 'POST',
                    headers: { 'X-Api-Key': API_KEY, 'Content-Type': 'application/json', 'Content-Length': envPayloadBuffer.length }
                }, envPayloadBuffer);
            }
            console.log(`[Environment Sync] Status: ${resEnv.statusCode}`);
        } catch (e) {
            console.error("Environment Deploy error:", e.message);
        }

    } catch (err) {
        console.error("Critical Sync Error:", err);
    }
}

run();
