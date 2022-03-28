using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamaVerification.Data.Migrations.Migrations
{
    public partial class add_customer_api_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "customer",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "customer",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "fk_customer_customers_api_keys_customer_id",
                table: "customer",
                column: "customer_id",
                principalTable: "customer_api_key",
                principalColumn: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_customers_api_keys_customer_id",
                table: "customer");

            migrationBuilder.DropTable(
                name: "customer_api_key");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "customer",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "false");
        }
    }
}
