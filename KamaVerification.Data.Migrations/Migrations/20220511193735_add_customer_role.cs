using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamaVerification.Data.Migrations.Migrations
{
    public partial class add_customer_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role_name",
                table: "customer",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "customer_role",
                columns: table => new
                {
                    role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_role", x => x.role_name);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_email_config_public_key",
                table: "customer_email_config",
                column: "public_key");

            migrationBuilder.CreateIndex(
                name: "ix_customer_public_key",
                table: "customer",
                column: "public_key");

            migrationBuilder.CreateIndex(
                name: "ix_customer_role_name",
                table: "customer",
                column: "role_name");

            migrationBuilder.CreateIndex(
                name: "ix_customer_role_public_key",
                table: "customer_role",
                column: "public_key");

            migrationBuilder.AddForeignKey(
                name: "fk_customer_customer_roles_role_temp_id",
                table: "customer",
                column: "role_name",
                principalTable: "customer_role",
                principalColumn: "role_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_customer_roles_role_temp_id",
                table: "customer");

            migrationBuilder.DropTable(
                name: "customer_role");

            migrationBuilder.DropIndex(
                name: "ix_customer_email_config_public_key",
                table: "customer_email_config");

            migrationBuilder.DropIndex(
                name: "ix_customer_public_key",
                table: "customer");

            migrationBuilder.DropIndex(
                name: "ix_customer_role_name",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "role_name",
                table: "customer");
        }
    }
}
