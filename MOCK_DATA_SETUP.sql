-- Sample Mock Data for Dictionary Management

-- 1. Ensure Table system.entity_dictionary exists
INSERT INTO system.entity_dictionary (id, entity_code, display_name, is_active)
VALUES 
('f5e3e2a0-7b5a-4b9a-8c9d-1e2f3a4b5c6d', 'catalog.unit', 'Đơn vị tính', true),
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'identity.user', 'Người dùng hệ thống', true)
ON CONFLICT (entity_code) DO UPDATE SET display_name = EXCLUDED.display_name;

-- 2. Mock fields for Catalog Unit
INSERT INTO system.entity_field_dictionary (entity_id, field_code, display_name, is_visible, is_required, order_index)
VALUES
('f5e3e2a0-7b5a-4b9a-8c9d-1e2f3a4b5c6d', 'code', 'Mã đơn vị', true, true, 1),
('f5e3e2a0-7b5a-4b9a-8c9d-1e2f3a4b5c6d', 'name', 'Tên đơn vị', true, true, 2),
('f5e3e2a0-7b5a-4b9a-8c9d-1e2f3a4b5c6d', 'precision', 'Độ chính xác', true, false, 3),
('f5e3e2a0-7b5a-4b9a-8c9d-1e2f3a4b5c6d', 'is_active', 'Đang hoạt động', true, false, 4)
ON CONFLICT (entity_id, field_code) DO NOTHING;

-- 3. Mock fields for Identity User
INSERT INTO system.entity_field_dictionary (entity_id, field_code, display_name, is_visible, is_required, order_index)
VALUES
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'username', 'Tên đăng nhập', true, true, 1),
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'full_name', 'Họ và tên', true, true, 2),
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'email', 'Địa chỉ Email', true, false, 3),
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'phone_number', 'Số điện thoại', true, false, 4),
('a1b2c3d4-e5f6-4a5b-b6c7-d8e9f0a1b2c3', 'dialing_code', 'Mã vùng', false, false, 5)
ON CONFLICT (entity_id, field_code) DO NOTHING;

-- 4. Fix missing columns in identity.user (from previous error)
ALTER TABLE identity."user" ADD COLUMN IF NOT EXISTS phone_number VARCHAR(32);
ALTER TABLE identity."user" ADD COLUMN IF NOT EXISTS dialing_code VARCHAR(10);
