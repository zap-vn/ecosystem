const { Client } = require('./node_modules/pg');
const c = new Client('postgres://postgres:Pg@Secret2026!@136.118.121.105:5432/zap_ecosystem_v200');
c.connect().then(async () => {
  const r = await c.query('SELECT column_name FROM information_schema.columns WHERE table_schema = \'people\' AND table_name = \'employee_profile\'');
  console.log(r.rows.map(x => x.column_name));
  process.exit(0);
});
