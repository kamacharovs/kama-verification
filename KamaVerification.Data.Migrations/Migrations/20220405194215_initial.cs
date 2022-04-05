using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamaVerification.Data.Migrations.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer_api_key",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    api_key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_api_key", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_email_config",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    subject = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    from_email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    from_name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    expiration_in_minutes = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "60"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_email_config", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_customer_customer_email_configs_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_email_config",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "fk_customer_customers_api_keys_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_api_key",
                        principalColumn: "customer_id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "customer_email_config");

            migrationBuilder.DropTable(
                name: "customer_api_key");
        }
    }
}
