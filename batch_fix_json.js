const fs = require('fs');
const path = require('path');

function walk(dir) {
    let results = [];
    const list = fs.readdirSync(dir);
    list.forEach(file => {
        file = path.join(dir, file);
        const stat = fs.statSync(file);
        if (stat && stat.isDirectory()) {
            results = results.concat(walk(file));
        } else {
            if (file.endsWith('.cs')) results.push(file);
        }
    });
    return results;
}

const srcDir = path.join(__dirname, 'src');
const files = walk(srcDir);

console.log(`Scanning ${files.length} files for JsonIgnore and JsonPropertyName...`);

let count = 0;
files.forEach(file => {
    let content = fs.readFileSync(file, 'utf8');
    let changed = false;

    // Replace JsonPropertyName -> JsonProperty
    if (content.includes('JsonPropertyName')) {
        content = content.replace(/\[JsonPropertyName\(([^)]+)\)\]/g, '[JsonProperty($1)]');
        changed = true;
    }

    // Replace System.Text.Json.Serialization -> Newtonsoft.Json
    if (content.includes('System.Text.Json.Serialization')) {
        content = content.replace(/using System\.Text\.Json\.Serialization;/g, 'using Newtonsoft.Json;');
        changed = true;
    }

    // We don't need to rename [JsonIgnore] because both libraries use the same attribute name, 
    // BUT we must ensure the using is correct.
    if (content.includes('[JsonIgnore]') && !content.includes('using Newtonsoft.Json;')) {
        // If it was using System.Text.Json.Serialization, it's already replaced above.
        // If it was using NOTHING (relying on implicit), we might need to add it or it might be in BaseEntity.
    }

    if (changed) {
        fs.writeFileSync(file, content);
        count++;
        console.log(`Updated ${file}`);
    }
});

console.log(`Successfully updated ${count} files.`);
