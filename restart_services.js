const { spawn } = require('child_process');
const path = require('path');

const projects = [
    { name: 'Gateway', path: 'src/Gateway/ZAP.Gateway/ZAP.Gateway.csproj' },
    { name: 'Identity', path: 'src/Modules/Identity/ZAP.Identity.API/ZAP.Identity.API.csproj' },
    { name: 'App', path: 'src/API/App/ZAP.Ecosystem.API.App/ZAP.Ecosystem.API.App.csproj' },
    { name: 'Modular', path: 'src/API/ZAP.Ecosystem.API/ZAP.Ecosystem.API.csproj' }
];

projects.forEach(p => {
    console.log(`Starting ${p.name}...`);
    const child = spawn('dotnet', ['run', '--project', p.path], {
        detached: true,
        stdio: 'ignore'
    });
    child.unref();
});

console.log('All services triggered in background.');
