async function testCreate() {
    const gateway = 'http://localhost:5120';
    
    try {
        console.log('--- Testing Create Unit ---');
        const response = await fetch(`${gateway}/api/v1/catalog/units`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: "BOX_TEST",
                name: "Thùng Test",
                precision: 0,
                is_active: true
            })
        });
        
        console.log('Status:', response.status);
        const data = await response.json();
        console.log('Result:', JSON.stringify(data, null, 2));
    } catch (error) {
        console.error('Error:', error.message);
    }
}

testCreate();
