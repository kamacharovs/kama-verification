using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KamaVerification.Data.Migrations.Migrations
{
    public partial class update_customer_email_config_pk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_customer_email_configs_customer_id",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_customer_email_config",
                table: "customer_email_config");

            migrationBuilder.DropColumn(
                name: "customer_email_config_id",
                table: "customer_email_config");

            migrationBuilder.AddPrimaryKey(
                name: "pk_customer_email_config",
                table: "customer_email_config",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_customer_customer_email_configs_customer_id",
                table: "customer",
                column: "customer_id",
                principalTable: "customer_email_config",
                principalColumn: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customer_customer_email_configs_customer_id",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_customer_email_config",
                table: "customer_email_config");

            migrationBuilder.AddColumn<int>(
                name: "customer_email_config_id",
                table: "customer_email_config",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_customer_email_config",
                table: "customer_email_config",
                column: "customer_email_config_id");

            migrationBuilder.AddForeignKey(
                name: "fk_customer_customer_email_configs_customer_id",
                table: "customer",
                column: "customer_id",
                principalTable: "customer_email_config",
                principalColumn: "customer_email_config_id");
        }
    }
}
