async function test() {
    try {
        const loginRes = await fetch('http://localhost:5178/api/v1/auth/login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                account: "binh.hoang.coo@pho24.com.vn",
                password: "123456"
            })
        });
        
        let loginDataText = await loginRes.text();
        console.log("Login HTTP Status:", loginRes.status);
        
        const loginData = JSON.parse(loginDataText);
        if (!loginData.data) {
            console.error("Login failed! Response:", loginDataText);
            return;
        }

        const token = loginData.data.token;
        console.log("Login successful, token:", token.substring(0, 50) + "...");

        console.log("Fetching products via Ecosystem API directly...");
        const prodRes = await fetch('http://localhost:5146/api/v1/catalog/products/list', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                page: 1,
                pageSize: 10
            })
        });
        
        const prodDataText = await prodRes.text();
        console.log("Product HTTP Status:", prodRes.status);
        console.log("Product Response Text:", prodDataText.substring(0, 1000));
        
    } catch (err) {
        console.error("FAILED:", err.message);
    }
}
test();
