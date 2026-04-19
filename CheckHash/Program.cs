using Npgsql;
using System;
class Program { 
    static void Main() { 
        using var conn = new NpgsqlConnection("Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v300");
        conn.Open();
        using var cmd = new NpgsqlCommand(@"
            DROP TABLE IF EXISTS system.entity_field_dictionary CASCADE;
            DROP TABLE IF EXISTS system.entity_dictionary CASCADE;
            CREATE SCHEMA IF NOT EXISTS system;

            CREATE TABLE IF NOT EXISTS system.entity_dictionary (
                id UUID PRIMARY KEY,
                created_at TIMESTAMP NOT NULL,
                updated_at TIMESTAMP,
                created_by VARCHAR(255),
                updated_by VARCHAR(255),
                is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
                
                serial_id SERIAL,
                serial_number VARCHAR(100),
                tenant_id UUID,
                schema_name VARCHAR(100) NOT NULL,
                table_name VARCHAR(100) NOT NULL,
                display_name VARCHAR(255) NOT NULL,
                description TEXT,
                is_active BOOLEAN NOT NULL DEFAULT TRUE
            );

            CREATE TABLE IF NOT EXISTS system.entity_field_dictionary (
                id UUID PRIMARY KEY,
                created_at TIMESTAMP NOT NULL,
                updated_at TIMESTAMP,
                created_by VARCHAR(255),
                updated_by VARCHAR(255),
                is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
                
                serial_id SERIAL,
                serial_number VARCHAR(100),
                entity_dictionary_id UUID NOT NULL REFERENCES system.entity_dictionary(id) ON DELETE CASCADE,
                field_name VARCHAR(100) NOT NULL,
                data_type VARCHAR(50) NOT NULL DEFAULT 'string',
                display_name VARCHAR(255) NOT NULL,
                ui_component_type VARCHAR(50) NOT NULL DEFAULT 'text',
                is_required BOOLEAN NOT NULL DEFAULT FALSE,
                is_visible_list BOOLEAN NOT NULL DEFAULT TRUE,
                is_visible_detail BOOLEAN NOT NULL DEFAULT TRUE,
                is_readonly BOOLEAN NOT NULL DEFAULT FALSE,
                is_searchable BOOLEAN NOT NULL DEFAULT FALSE,
                is_filterable BOOLEAN NOT NULL DEFAULT FALSE,
                is_sortable BOOLEAN NOT NULL DEFAULT FALSE,
                default_value TEXT,
                min_length INT,
                max_length INT,
                regex_pattern VARCHAR(255),
                lookup_reference VARCHAR(255),
                tooltip_text TEXT,
                sort_order INT NOT NULL DEFAULT 0
            );
        ", conn);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Tables created successfully.");
    }
}
