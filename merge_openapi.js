const fs = require('fs');
const http = require('http');

async function fetchJson(url) {
  return new Promise((resolve, reject) => {
    http.get(url, (res) => {
      let data = '';
      res.on('data', (chunk) => data += chunk);
      res.on('end', () => {
        try {
          resolve(JSON.parse(data));
        } catch (e) {
          reject(e);
        }
      });
    }).on('error', (err) => reject(err));
  });
}

async function mergeSpecs() {
  const ports = [5146, 5181, 5182];
  const specs = [];

  for (const port of ports) {
    try {
      console.log(`Fetching from ${port}...`);
      const spec = await fetchJson(`http://localhost:${port}/openapi/v1.json`);
      specs.push(spec);
    } catch (e) {
      console.error(`Failed to fetch from ${port}: ${e.message}`);
    }
  }

  if (specs.length === 0) {
    console.error('No specs fetched. Aborting.');
    return;
  }

  // Use the first spec as the base
  const masterSpec = {
    openapi: '3.0.0',
    info: {
      title: 'ZAP Ecosystem Aggregate API',
      version: 'v1.0.0',
      description: 'Unified API across CRM, Identity, and App modules'
    },
    paths: {},
    components: {
      schemas: {},
      securitySchemes: specs[0].components?.securitySchemes || {}
    }
  };

  specs.forEach(spec => {
    // Merge paths
    if (spec.paths) {
      Object.entries(spec.paths).forEach(([path, methods]) => {
        masterSpec.paths[path] = methods;
      });
    }
    // Merge schemas
    if (spec.components && spec.components.schemas) {
      Object.entries(spec.components.schemas).forEach(([name, schema]) => {
        masterSpec.components.schemas[name] = schema;
      });
    }
  });

  fs.writeFileSync('openapi.json', JSON.stringify(masterSpec, null, 2));
  console.log('Merged openapi.json generated successfully.');
}

mergeSpecs();
