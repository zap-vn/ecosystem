-- SQL Script to create Entity & Field Dictionary Tables
-- Schema: system
-- Purpose: Manage labels, visibility, and requirements of columns across tables.

CREATE SCHEMA IF NOT EXISTS "system";

-- 1. Table: system.entity_dictionary
-- Stores the mapping of logical entities (e.g., catalog.product) to human-readable names.
CREATE TABLE IF NOT EXISTS system.entity_dictionary (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    entity_code VARCHAR(100) UNIQUE NOT NULL, -- Logical code (e.g., 'catalog.product', 'identity.user')
    display_name VARCHAR(255) NOT NULL,      -- Display Label (e.g., 'Sản phẩm', 'Người dùng')
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. Table: system.entity_field_dictionary
-- Stores the configuration for specific fields (columns) within an entity.
CREATE TABLE IF NOT EXISTS system.entity_field_dictionary (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    entity_id UUID NOT NULL REFERENCES system.entity_dictionary(id) ON DELETE CASCADE,
    field_code VARCHAR(100) NOT NULL,        -- Property/Column code (e.g., 'serial_number', 'phone_number')
    display_name VARCHAR(255) NOT NULL,      -- Field Label (e.g., 'Mã Serial', 'Số điện thoại')
    is_visible BOOLEAN DEFAULT TRUE,         -- Whether to show the column in UI
    is_required BOOLEAN DEFAULT FALSE,       -- Whether the field is mandatory
    order_index INTEGER DEFAULT 0,           -- Display order
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(entity_id, field_code)
);

-- Initial indexing for performance
CREATE INDEX IF NOT EXISTS idx_entity_field_entity_id ON system.entity_field_dictionary(entity_id);

-- Descriptive comments
COMMENT ON TABLE system.entity_dictionary IS 'Quản lý danh sách các entity (table) trong hệ thống';
COMMENT ON TABLE system.entity_field_dictionary IS 'Quản lý danh sách các field (column) và cấu hình hiển thị theo entity';
