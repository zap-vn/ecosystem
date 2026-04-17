const https = require('https');
const fs = require('fs');

const API_KEY = 'PMAK-69e22f257e44dc0001c11bbd-8add08c0a15d437deba43d3267b8be7a09';
const COLLECTION_FILE = 'ZAP_Ecosystem_Postman_Collection.json';

function request(options, data = null) {
  return new Promise((resolve, reject) => {
    const req = https.request(options, (res) => {
      let body = '';
      res.on('data', (chunk) => body += chunk);
      res.on('end', () => {
        try {
          resolve(JSON.parse(body));
        } catch (e) {
          resolve(body);
        }
      });
    });
    req.on('error', reject);
    if (data) req.write(typeof data === 'string' ? data : JSON.stringify(data));
    req.end();
  });
}

async function deploy() {
  console.log('Fetching collections...');
  const collectionsData = await request({
    hostname: 'api.getpostman.com',
    path: '/collections',
    method: 'GET',
    headers: { 'X-Api-Key': API_KEY }
  });

  if (!collectionsData.collections) {
    console.error('Failed to fetch collections:', collectionsData);
    return;
  }

  // Find the collection matching the user's workspace or name
  // The user provided a URL with request ID: 15026203-8a6e6e3c-4938-484b-88c1-f3717b7c004f
  // Usually the collection UID is the prefix.
  let collectionUid = '8a6e6e3c-4938-484b-88c1-f3717b7c004f'; // Based on the URL provided
  
  const target = collectionsData.collections.find(c => 
    c.uid === collectionUid || c.name.toLowerCase().includes('zap') || c.name.toLowerCase().includes('ecosystem')
  );

  if (!target) {
    console.log('Exact UID not found in basic list, searching by name...');
    const matchByName = collectionsData.collections.find(c => 
        c.name.toLowerCase().includes('zap') || c.name.toLowerCase().includes('ecosystem')
    );
    if (matchByName) {
        collectionUid = matchByName.uid;
        console.log(`Found matching collection by name: ${matchByName.name} (${collectionUid})`);
    } else {
        console.error('Could not find a matching collection. Available names:', collectionsData.collections.map(c => c.name));
        return;
    }
  } else {
    collectionUid = target.uid;
    console.log(`Found target collection: ${target.name} (${collectionUid})`);
  }

  console.log('Reading local collection file...');
  const localData = JSON.parse(fs.readFileSync(COLLECTION_FILE, 'utf8'));

  // Wrap in the standard Postman update format
  const updatePayload = {
    collection: localData
  };

  console.log(`Updating collection ${collectionUid} in Postman Cloud...`);
  const result = await request({
    hostname: 'api.getpostman.com',
    path: `/collections/${collectionUid}`,
    method: 'PUT',
    headers: { 
      'X-Api-Key': API_KEY,
      'Content-Type': 'application/json'
    }
  }, updatePayload);

  if (result.collection && result.collection.uid) {
    console.log('✅ Postman Cloud synchronization SUCCESSFUL!');
    console.log(`Collection Name: ${result.collection.name}`);
    console.log(`Collection UID: ${result.collection.uid}`);
  } else {
    console.error('❌ Postman Cloud synchronization FAILED:', result);
  }
}

deploy();
