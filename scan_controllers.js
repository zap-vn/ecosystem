const fs = require('fs');
const path = require('path');

const rootDir = 'src';
const controllers = [];

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
    
    // Simple heuristic to find actions
    const lines = content.split('\n');
    let currentAction = null;

    lines.forEach(line => {
        const httpMatch = line.match(/\[Http(Get|Post|Put|Delete)(?:\("(.*?)"\))?\]/);
        if (httpMatch) {
            currentAction = {
                method: httpMatch[1].toUpperCase(),
                subRoute: httpMatch[2] || '',
                baseRoute: baseRoute,
                fullRoute: (baseRoute + '/' + (httpMatch[2] || '')).replace(/\/+/g, '/').replace(/\/$/, '')
            };
        } else if (currentAction && line.includes('public async Task<IActionResult>')) {
            const nameMatch = line.match(/public async Task<IActionResult>\s+(\w+)\s*\((.*?)\)/);
            if (nameMatch) {
                currentAction.name = nameMatch[1];
                currentAction.params = nameMatch[2];
                controllers.push({...currentAction, file: path.basename(filePath)});
                currentAction = null;
            }
        }
    });
}

scanDir(rootDir);

// Filter by Module (based on path)
const modules = {};
controllers.forEach(c => {
    const parts = c.fullRoute.split('/');
    const moduleName = parts[3] || 'Other';
    if (!modules[moduleName]) modules[moduleName] = [];
    modules[moduleName].push(c);
});

console.log('--- FOUND ENDPOINTS ---');
Object.entries(modules).forEach(([mod, endpoints]) => {
    console.log(`\nModule: ${mod.toUpperCase()}`);
    endpoints.forEach(e => {
        console.log(`  [${e.method}] ${e.fullRoute} (${e.name})`);
    });
});

// Now compare with a list of "What should be there"
// Or just output this to the user to confirm.
