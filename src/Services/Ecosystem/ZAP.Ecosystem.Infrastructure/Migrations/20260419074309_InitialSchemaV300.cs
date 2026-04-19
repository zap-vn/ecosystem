using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZAP.Ecosystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaV300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.EnsureSchema(
                name: "people");

            migrationBuilder.EnsureSchema(
                name: "customers");

            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.EnsureSchema(
                name: "commerce");

            migrationBuilder.EnsureSchema(
                name: "hr");

            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.EnsureSchema(
                name: "locations");

            migrationBuilder.EnsureSchema(
                name: "payments");

            migrationBuilder.EnsureSchema(
                name: "platform");

            migrationBuilder.EnsureSchema(
                name: "promotions");

            migrationBuilder.EnsureSchema(
                name: "reports");

            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dialing_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_groups",
                schema: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    discount_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dining_option",
                schema: "sales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dining_option", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "hr",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    employee_code = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    department = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entity_dictionary",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    schema_name = table.Column<string>(type: "text", nullable: false),
                    table_name = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_dictionary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "geo_country",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    iso_alpha2 = table.Column<string>(type: "text", nullable: true),
                    iso_alpha3 = table.Column<string>(type: "text", nullable: true),
                    numeric_code = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    geometry_data = table.Column<string>(type: "text", nullable: true),
                    flag_emoji = table.Column<string>(type: "text", nullable: true),
                    flag_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_geo_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "geo_province",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_geo_province", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "location_entities",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lookups",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loyalty_level",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    level_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    min_points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_loyalty_level", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loyalty_tiers",
                schema: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tier_name = table.Column<string>(type: "text", nullable: false),
                    tier_code = table.Column<string>(type: "text", nullable: true),
                    priority_level = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_loyalty_tiers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modifier_item",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    price_override = table.Column<decimal>(type: "numeric", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modifier_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_detail_entities",
                schema: "sales",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    order_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_detail_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_entities",
                schema: "sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<long>(type: "bigint", nullable: false),
                    order_code = table.Column<string>(type: "text", nullable: false),
                    cart_id = table.Column<string>(type: "text", nullable: true),
                    dining_option_id = table.Column<int>(type: "integer", nullable: false),
                    customer_guest_guid = table.Column<string>(type: "text", nullable: true),
                    location_guid = table.Column<string>(type: "text", nullable: true),
                    device_guid = table.Column<string>(type: "text", nullable: true),
                    assign_to_location_guid = table.Column<string>(type: "text", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    order_status_id = table.Column<int>(type: "integer", nullable: false),
                    payment_status_id = table.Column<int>(type: "integer", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization_units",
                schema: "hr",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                schema: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    icon = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_termss",
                schema: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    days = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_termss", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_types",
                schema: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_entities",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    emp_guid = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    barcode = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    visible = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_type_item",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_type_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promotion",
                schema: "promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    terms_conditions = table.Column<string>(type: "text", nullable: true),
                    color_hex = table.Column<string>(type: "text", nullable: true),
                    reference_id = table.Column<string>(type: "text", nullable: true),
                    promotion_class_id = table.Column<int>(type: "integer", nullable: false),
                    discount_type_id = table.Column<int>(type: "integer", nullable: true),
                    apply_to_id = table.Column<int>(type: "integer", nullable: true),
                    campaign_type_id = table.Column<int>(type: "integer", nullable: true),
                    min_requirement_type_id = table.Column<int>(type: "integer", nullable: true),
                    is_automatic = table.Column<bool>(type: "boolean", nullable: false),
                    is_scan_qr_table = table.Column<bool>(type: "boolean", nullable: false),
                    is_visible_pos = table.Column<bool>(type: "boolean", nullable: false),
                    is_banner_default = table.Column<bool>(type: "boolean", nullable: false),
                    is_exclude_mode = table.Column<bool>(type: "boolean", nullable: false),
                    discount_value = table.Column<decimal>(type: "numeric", nullable: false),
                    maximum_discount_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    is_discount_limit = table.Column<bool>(type: "boolean", nullable: false),
                    only_apply_once_per_order = table.Column<bool>(type: "boolean", nullable: false),
                    min_requirement_value = table.Column<decimal>(type: "numeric", nullable: false),
                    is_all_locations = table.Column<bool>(type: "boolean", nullable: false),
                    is_all_payment_methods = table.Column<bool>(type: "boolean", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_code = table.Column<string>(type: "text", nullable: true),
                    status_name = table.Column<string>(type: "text", nullable: true),
                    created_at1 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at1 = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                schema: "promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    discount_value = table.Column<decimal>(type: "numeric", nullable: false),
                    discount_type = table.Column<string>(type: "text", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_templates",
                schema: "reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    configuration_json = table.Column<string>(type: "text", nullable: false),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status_item",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: true),
                    domain = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_group_translations",
                schema: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    customer_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_group_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_group_translations_customer_groups_customer_group_",
                        column: x => x.customer_group_id,
                        principalSchema: "customers",
                        principalTable: "customer_groups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "dining_option_translation",
                schema: "commerce",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dining_option_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    dining_optionid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dining_option_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_dining_option_translation_dining_option_dining_optionid",
                        column: x => x.dining_optionid,
                        principalSchema: "sales",
                        principalTable: "dining_option",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "employee_translations",
                schema: "hr",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    department = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_employee_translations_employees_employee_id",
                        column: x => x.employee_id,
                        principalSchema: "hr",
                        principalTable: "employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "entity_field_dictionary",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    entity_dictionary_id = table.Column<Guid>(type: "uuid", nullable: false),
                    field_name = table.Column<string>(type: "text", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    ui_component_type = table.Column<string>(type: "text", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    is_visible_list = table.Column<bool>(type: "boolean", nullable: false),
                    is_visible_detail = table.Column<bool>(type: "boolean", nullable: false),
                    is_readonly = table.Column<bool>(type: "boolean", nullable: false),
                    is_searchable = table.Column<bool>(type: "boolean", nullable: false),
                    is_filterable = table.Column<bool>(type: "boolean", nullable: false),
                    is_sortable = table.Column<bool>(type: "boolean", nullable: false),
                    default_value = table.Column<string>(type: "text", nullable: true),
                    min_length = table.Column<int>(type: "integer", nullable: true),
                    max_length = table.Column<int>(type: "integer", nullable: true),
                    regex_pattern = table.Column<string>(type: "text", nullable: true),
                    lookup_reference = table.Column<string>(type: "text", nullable: true),
                    tooltip_text = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_field_dictionary", x => x.id);
                    table.ForeignKey(
                        name: "fk_entity_field_dictionary_entity_dictionary_entity_dictionary",
                        column: x => x.entity_dictionary_id,
                        principalSchema: "system",
                        principalTable: "entity_dictionary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "geo_country_translation",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_geo_country_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_geo_country_translation_geo_country_country_id",
                        column: x => x.country_id,
                        principalSchema: "system",
                        principalTable: "geo_country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "geo_province_translation",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    province_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    province_itemid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_geo_province_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_geo_province_translation_geo_province_province_itemid",
                        column: x => x.province_itemid,
                        principalSchema: "locations",
                        principalTable: "geo_province",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lookup_translations",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lookup_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookup_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_lookup_translations_lookups_lookup_id",
                        column: x => x.lookup_id,
                        principalSchema: "locations",
                        principalTable: "lookups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_membership",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loyalty_level_id = table.Column<Guid>(type: "uuid", nullable: true),
                    current_points = table.Column<int>(type: "integer", nullable: false),
                    joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_membership", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_membership_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "identity",
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_membership_loyalty_level_loyalty_level_id",
                        column: x => x.loyalty_level_id,
                        principalSchema: "identity",
                        principalTable: "loyalty_level",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "organization_unit_translations",
                schema: "hr",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    organization_unit_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_unit_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_organization_unit_translations_organization_units_organizat",
                        column: x => x.organization_unit_id,
                        principalSchema: "hr",
                        principalTable: "organization_units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "payment_method_translations",
                schema: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_method_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_method_translations_payment_methods_payment_method_",
                        column: x => x.payment_method_id,
                        principalSchema: "payments",
                        principalTable: "payment_methods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "payment_terms_translates",
                schema: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    payment_terms_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_terms_translates", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_terms_translates_payment_termss_payment_terms_id",
                        column: x => x.payment_terms_id,
                        principalSchema: "payments",
                        principalTable: "payment_termss",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "translate_payment_types",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    payment_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translate_payment_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_translate_payment_types_payment_types_payment_type_id",
                        column: x => x.payment_type_id,
                        principalSchema: "payments",
                        principalTable: "payment_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_translations",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    product_entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_translations_product_entities_product_entity_id",
                        column: x => x.product_entity_id,
                        principalSchema: "catalog",
                        principalTable: "product_entities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_type_translation",
                schema: "platform",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_type_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    product_type_itemid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_type_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_type_translation_product_type_item_product_type_ite",
                        column: x => x.product_type_itemid,
                        principalSchema: "catalog",
                        principalTable: "product_type_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promotion_translation",
                schema: "promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    promotion_entityid = table.Column<Guid>(type: "uuid", nullable: true),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_promotion_translation_promotion_promotion_entityid",
                        column: x => x.promotion_entityid,
                        principalSchema: "promotions",
                        principalTable: "promotion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_promotion_translation_promotions_promotion_id",
                        column: x => x.promotion_id,
                        principalSchema: "promotions",
                        principalTable: "promotions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "report_template_translations",
                schema: "reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    report_template_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_template_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_report_template_translations_report_templates_report_templa",
                        column: x => x.report_template_id,
                        principalSchema: "reports",
                        principalTable: "report_templates",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "brand",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: false),
                    banner_url = table.Column<string>(type: "text", nullable: false),
                    website_url = table.Column<string>(type: "text", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    is_premium = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand", x => x.id);
                    table.ForeignKey(
                        name: "fk_brand_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    category_code = table.Column<string>(type: "text", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    materialized_path = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: true),
                    icon_url = table.Column<string>(type: "text", nullable: true),
                    banner_url = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: true),
                    canonical_url = table.Column<string>(type: "text", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    applicable_channels = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_category_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_category_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "collection",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collection", x => x.id);
                    table.ForeignKey(
                        name: "fk_collection_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "people",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    customer_code = table.Column<string>(type: "text", nullable: false),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    square_customer_id = table.Column<string>(type: "text", nullable: true),
                    reference_id = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    nickname = table.Column<string>(type: "text", nullable: true),
                    company_name = table.Column<string>(type: "text", nullable: true),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    gender_id = table.Column<int>(type: "integer", nullable: true),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    address_line_1 = table.Column<string>(type: "text", nullable: true),
                    address_line_2 = table.Column<string>(type: "text", nullable: true),
                    city_name = table.Column<string>(type: "text", nullable: true),
                    state_name = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    province_id = table.Column<int>(type: "integer", nullable: true),
                    district_id = table.Column<int>(type: "integer", nullable: true),
                    ward_id = table.Column<int>(type: "integer", nullable: true),
                    zipcode = table.Column<string>(type: "text", nullable: true),
                    preferred_locale_id = table.Column<int>(type: "integer", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    memo = table.Column<string>(type: "text", nullable: true),
                    creation_source = table.Column<string>(type: "text", nullable: false),
                    email_subscription_status = table.Column<string>(type: "text", nullable: true),
                    is_instant_profile = table.Column<bool>(type: "boolean", nullable: false),
                    current_points_balance = table.Column<decimal>(type: "numeric", nullable: false),
                    total_spent_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    average_spent_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_visits_count = table.Column<int>(type: "integer", nullable: false),
                    first_visit_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_visit_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at1 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at1 = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    loyalty_tierid = table.Column<Guid>(type: "uuid", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: true),
                    user_guid = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_loyalty_tiers_loyalty_tierid",
                        column: x => x.loyalty_tierid,
                        principalSchema: "customers",
                        principalTable: "loyalty_tiers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_customer_status_item_statusid",
                        column: x => x.statusid,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "location",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<long>(type: "bigint", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: false),
                    location_code = table.Column<string>(type: "text", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    node_id = table.Column<Guid>(type: "uuid", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    business_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location_type_id = table.Column<int>(type: "integer", nullable: true),
                    address_line_1 = table.Column<string>(type: "text", nullable: true),
                    address_line_2 = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    province_id = table.Column<int>(type: "integer", nullable: true),
                    district_id = table.Column<int>(type: "integer", nullable: true),
                    ward_id = table.Column<int>(type: "integer", nullable: true),
                    zipcode = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    website = table.Column<string>(type: "text", nullable: true),
                    twitter = table.Column<string>(type: "text", nullable: true),
                    instagram = table.Column<string>(type: "text", nullable: true),
                    facebook = table.Column<string>(type: "text", nullable: true),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    cover_image_url = table.Column<string>(type: "text", nullable: true),
                    brand_color = table.Column<string>(type: "text", nullable: true),
                    timezone = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    operating_hours = table.Column<string>(type: "jsonb", nullable: true),
                    transfer_account = table.Column<string>(type: "text", nullable: true),
                    transfer_tag = table.Column<string>(type: "text", nullable: true),
                    parent_location_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_lookups_location_type_id",
                        column: x => x.location_type_id,
                        principalSchema: "locations",
                        principalTable: "lookups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_location_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu_header",
                schema: "sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    menu_type = table.Column<string>(type: "text", nullable: false),
                    app_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    timezone_id = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_header", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_header_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "modifier_group",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    min_selection = table.Column<int>(type: "integer", nullable: false),
                    max_selection = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modifier_group", x => x.id);
                    table.ForeignKey(
                        name: "fk_modifier_group_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    product_type_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: true),
                    long_description_html = table.Column<string>(type: "text", nullable: true),
                    search_vector = table.Column<string>(type: "text", nullable: true),
                    priority_score = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    is_featured = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: true),
                    product_typeid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_product_type_item_product_typeid",
                        column: x => x.product_typeid,
                        principalSchema: "catalog",
                        principalTable: "product_type_item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_status_item_statusid",
                        column: x => x.statusid,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "status_item_translation",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status_item_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status_item_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_status_item_translation_status_item_status_item_id",
                        column: x => x.status_item_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uom_item",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    abbreviation = table.Column<string>(type: "text", nullable: true),
                    precision = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uom_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_uom_item_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "collection_item",
                schema: "catalog",
                columns: table => new
                {
                    collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    collectionid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collection_item", x => new { x.collection_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_collection_item_collection_collectionid",
                        column: x => x.collectionid,
                        principalSchema: "catalog",
                        principalTable: "collection",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "customer_translations",
                schema: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    customer_entityid = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_translations_customer_customer_entityid",
                        column: x => x.customer_entityid,
                        principalSchema: "people",
                        principalTable: "customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "store",
                schema: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: true),
                    legacy_id = table.Column<string>(type: "text", nullable: true),
                    store_code = table.Column<string>(type: "text", nullable: true),
                    store_name = table.Column<string>(type: "text", nullable: true),
                    address_line_1 = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    business_address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    node_type_id = table.Column<int>(type: "integer", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    province_id = table.Column<int>(type: "integer", nullable: true),
                    district_id = table.Column<int>(type: "integer", nullable: true),
                    ward_id = table.Column<int>(type: "integer", nullable: true),
                    timezone = table.Column<string>(type: "text", nullable: true),
                    opening_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    closing_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_store", x => x.id);
                    table.ForeignKey(
                        name: "fk_store_location_location_id",
                        column: x => x.location_id,
                        principalSchema: "locations",
                        principalTable: "location",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_store_status_item_status_id",
                        column: x => x.status_id,
                        principalSchema: "core",
                        principalTable: "status_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "menu_availability_schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    menuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_availability_schedule", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_availability_schedule_menu_header_menuid",
                        column: x => x.menuid,
                        principalSchema: "sales",
                        principalTable: "menu_header",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu_section",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    menuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_section", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_section_menu_header_menuid",
                        column: x => x.menuid,
                        principalSchema: "sales",
                        principalTable: "menu_header",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_category_map",
                schema: "catalog",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: true),
                    categoryid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category_map", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_product_category_map_category_categoryid",
                        column: x => x.categoryid,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_category_map_product_productid",
                        column: x => x.productid,
                        principalSchema: "catalog",
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_variants",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_id = table.Column<int>(type: "integer", nullable: true),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sku_code = table.Column<string>(type: "text", nullable: true),
                    barcode = table.Column<string>(type: "text", nullable: true),
                    variant_name = table.Column<string>(type: "text", nullable: true),
                    base_price = table.Column<decimal>(type: "numeric", nullable: true),
                    sale_price = table.Column<decimal>(type: "numeric", nullable: true),
                    cost_price = table.Column<decimal>(type: "numeric", nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    weight_grams = table.Column<decimal>(type: "numeric", nullable: true),
                    length_mm = table.Column<decimal>(type: "numeric", nullable: true),
                    width_mm = table.Column<decimal>(type: "numeric", nullable: true),
                    height_mm = table.Column<decimal>(type: "numeric", nullable: true),
                    attributes = table.Column<string>(type: "text", nullable: true),
                    uom_id = table.Column<int>(type: "integer", nullable: true),
                    uomid = table.Column<int>(type: "integer", nullable: true),
                    productid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variants", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_variants_product_productid",
                        column: x => x.productid,
                        principalSchema: "catalog",
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_variants_uom_item_uomid",
                        column: x => x.uomid,
                        principalSchema: "catalog",
                        principalTable: "uom_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "uom_item_translation",
                schema: "platform",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    uom_item_id = table.Column<int>(type: "integer", nullable: false),
                    locale_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    uom_itemid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uom_item_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_uom_item_translation_uom_item_uom_itemid",
                        column: x => x.uom_itemid,
                        principalSchema: "catalog",
                        principalTable: "uom_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "bom_header",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    uom_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    variantid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bom_header", x => x.id);
                    table.ForeignKey(
                        name: "fk_bom_header_product_variants_variantid",
                        column: x => x.variantid,
                        principalSchema: "catalog",
                        principalTable: "product_variants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "inventory_item",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qty_on_hand = table.Column<decimal>(type: "numeric", nullable: false),
                    variantid = table.Column<Guid>(type: "uuid", nullable: true),
                    locationid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_inventory_item_location_locationid",
                        column: x => x.locationid,
                        principalSchema: "locations",
                        principalTable: "location",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_inventory_item_product_variants_variantid",
                        column: x => x.variantid,
                        principalSchema: "catalog",
                        principalTable: "product_variants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "menu_item_hds",
                schema: "sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    base_price = table.Column<decimal>(type: "numeric", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    variantid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_hds", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_item_hds_product_variants_variantid",
                        column: x => x.variantid,
                        principalSchema: "catalog",
                        principalTable: "product_variants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_location_pricing",
                schema: "catalog",
                columns: table => new
                {
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_price_override = table.Column<decimal>(type: "numeric", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    variantid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_location_pricing", x => new { x.product_variant_id, x.location_id });
                    table.ForeignKey(
                        name: "fk_product_location_pricing_product_variants_variantid",
                        column: x => x.variantid,
                        principalSchema: "catalog",
                        principalTable: "product_variants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_media",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    media_url = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    variantid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_media", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_media_product_variants_variantid",
                        column: x => x.variantid,
                        principalSchema: "catalog",
                        principalTable: "product_variants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_bom_header_variantid",
                schema: "inventory",
                table: "bom_header",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "ix_brand_status_id",
                schema: "catalog",
                table: "brand",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_parent_id",
                schema: "catalog",
                table: "category",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_status_id",
                schema: "catalog",
                table: "category",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_collection_status_id",
                schema: "catalog",
                table: "collection",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_collection_item_collectionid",
                schema: "catalog",
                table: "collection_item",
                column: "collectionid");

            migrationBuilder.CreateIndex(
                name: "ix_customer_loyalty_tierid",
                schema: "people",
                table: "customer",
                column: "loyalty_tierid");

            migrationBuilder.CreateIndex(
                name: "ix_customer_statusid",
                schema: "people",
                table: "customer",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_customer_group_translations_customer_group_id",
                schema: "customers",
                table: "customer_group_translations",
                column: "customer_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_membership_customer_id",
                schema: "identity",
                table: "customer_membership",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customer_membership_loyalty_level_id",
                schema: "identity",
                table: "customer_membership",
                column: "loyalty_level_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_translations_customer_entityid",
                schema: "customers",
                table: "customer_translations",
                column: "customer_entityid");

            migrationBuilder.CreateIndex(
                name: "ix_dining_option_translation_dining_optionid",
                schema: "commerce",
                table: "dining_option_translation",
                column: "dining_optionid");

            migrationBuilder.CreateIndex(
                name: "ix_employee_translations_employee_id",
                schema: "hr",
                table: "employee_translations",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_entity_field_dictionary_entity_dictionary_id",
                schema: "system",
                table: "entity_field_dictionary",
                column: "entity_dictionary_id");

            migrationBuilder.CreateIndex(
                name: "ix_geo_country_translation_country_id",
                schema: "system",
                table: "geo_country_translation",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_geo_province_translation_province_itemid",
                schema: "locations",
                table: "geo_province_translation",
                column: "province_itemid");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_item_locationid",
                schema: "inventory",
                table: "inventory_item",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_item_variantid",
                schema: "inventory",
                table: "inventory_item",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "ix_location_location_type_id",
                schema: "locations",
                table: "location",
                column: "location_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_status_id",
                schema: "locations",
                table: "location",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_lookup_translations_lookup_id",
                schema: "locations",
                table: "lookup_translations",
                column: "lookup_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_availability_schedule_menuid",
                table: "menu_availability_schedule",
                column: "menuid");

            migrationBuilder.CreateIndex(
                name: "ix_menu_header_status_id",
                schema: "sales",
                table: "menu_header",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_hds_variantid",
                schema: "sales",
                table: "menu_item_hds",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "ix_menu_section_menuid",
                table: "menu_section",
                column: "menuid");

            migrationBuilder.CreateIndex(
                name: "ix_modifier_group_status_id",
                schema: "catalog",
                table: "modifier_group",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_unit_translations_organization_unit_id",
                schema: "hr",
                table: "organization_unit_translations",
                column: "organization_unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_method_translations_payment_method_id",
                schema: "payments",
                table: "payment_method_translations",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_terms_translates_payment_terms_id",
                schema: "payments",
                table: "payment_terms_translates",
                column: "payment_terms_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_typeid",
                schema: "catalog",
                table: "product",
                column: "product_typeid");

            migrationBuilder.CreateIndex(
                name: "ix_product_statusid",
                schema: "catalog",
                table: "product",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_map_categoryid",
                schema: "catalog",
                table: "product_category_map",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_map_productid",
                schema: "catalog",
                table: "product_category_map",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_product_location_pricing_variantid",
                schema: "catalog",
                table: "product_location_pricing",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "ix_product_media_variantid",
                schema: "catalog",
                table: "product_media",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "ix_product_translations_product_entity_id",
                schema: "catalog",
                table: "product_translations",
                column: "product_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_type_translation_product_type_itemid",
                schema: "platform",
                table: "product_type_translation",
                column: "product_type_itemid");

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_productid",
                schema: "catalog",
                table: "product_variants",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_uomid",
                schema: "catalog",
                table: "product_variants",
                column: "uomid");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_translation_promotion_entityid",
                schema: "promotions",
                table: "promotion_translation",
                column: "promotion_entityid");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_translation_promotion_id",
                schema: "promotions",
                table: "promotion_translation",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_report_template_translations_report_template_id",
                schema: "reports",
                table: "report_template_translations",
                column: "report_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_status_item_translation_status_item_id",
                schema: "system",
                table: "status_item_translation",
                column: "status_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_store_location_id",
                schema: "locations",
                table: "store",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_store_status_id",
                schema: "locations",
                table: "store",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_translate_payment_types_payment_type_id",
                schema: "core",
                table: "translate_payment_types",
                column: "payment_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_uom_item_status_id",
                schema: "catalog",
                table: "uom_item",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_uom_item_translation_uom_itemid",
                schema: "platform",
                table: "uom_item_translation",
                column: "uom_itemid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bom_header",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "brand",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "collection_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "customer_group_translations",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "customer_membership",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "customer_translations",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "dining_option_translation",
                schema: "commerce");

            migrationBuilder.DropTable(
                name: "employee_translations",
                schema: "hr");

            migrationBuilder.DropTable(
                name: "entity_field_dictionary",
                schema: "system");

            migrationBuilder.DropTable(
                name: "geo_country_translation",
                schema: "system");

            migrationBuilder.DropTable(
                name: "geo_province_translation",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "inventory_item",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "location_entities",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "lookup_translations",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "menu_availability_schedule");

            migrationBuilder.DropTable(
                name: "menu_item_hds",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "menu_section");

            migrationBuilder.DropTable(
                name: "modifier_group",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "modifier_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "order_detail_entities",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "order_entities",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "organization_unit_translations",
                schema: "hr");

            migrationBuilder.DropTable(
                name: "payment_method_translations",
                schema: "payments");

            migrationBuilder.DropTable(
                name: "payment_terms_translates",
                schema: "payments");

            migrationBuilder.DropTable(
                name: "product_category_map",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_location_pricing",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_media",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_translations",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_type_translation",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "promotion_translation",
                schema: "promotions");

            migrationBuilder.DropTable(
                name: "report_template_translations",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "status_item_translation",
                schema: "system");

            migrationBuilder.DropTable(
                name: "store",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "translate_payment_types",
                schema: "core");

            migrationBuilder.DropTable(
                name: "uom_item_translation",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "collection",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "customer_groups",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "loyalty_level",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "people");

            migrationBuilder.DropTable(
                name: "dining_option",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "employees",
                schema: "hr");

            migrationBuilder.DropTable(
                name: "entity_dictionary",
                schema: "system");

            migrationBuilder.DropTable(
                name: "geo_country",
                schema: "system");

            migrationBuilder.DropTable(
                name: "geo_province",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "menu_header",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "organization_units",
                schema: "hr");

            migrationBuilder.DropTable(
                name: "payment_methods",
                schema: "payments");

            migrationBuilder.DropTable(
                name: "payment_termss",
                schema: "payments");

            migrationBuilder.DropTable(
                name: "category",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_variants",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_entities",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "promotion",
                schema: "promotions");

            migrationBuilder.DropTable(
                name: "promotions",
                schema: "promotions");

            migrationBuilder.DropTable(
                name: "report_templates",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "location",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "payment_types",
                schema: "payments");

            migrationBuilder.DropTable(
                name: "loyalty_tiers",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "product",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "uom_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "lookups",
                schema: "locations");

            migrationBuilder.DropTable(
                name: "product_type_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "status_item",
                schema: "core");
        }
    }
}
