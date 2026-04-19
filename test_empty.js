async function testEmptyBody() {
    const gateway = 'http://localhost:5120';
    
    try {
        console.log('--- Testing Get Units List (Empty Body) ---');
        const response = await fetch(`${gateway}/api/v1/catalog/units/list`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({}) // Empty body
        });
        
        console.log('Status:', response.status);
        const data = await response.json();
        console.log('Result:', JSON.stringify(data, null, 2));
    } catch (error) {
        console.error('Error:', error.message);
    }
}

testEmptyBody();
