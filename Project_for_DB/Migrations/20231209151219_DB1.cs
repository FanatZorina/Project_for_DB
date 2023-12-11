using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_for_DB.Migrations
{
    /// <inheritdoc />
    public partial class DB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adress",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    building = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("adress_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departament",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("departament_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lock",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    id_street = table.Column<int>(type: "integer", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    closed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lock_pkey", x => new { x.id, x.id_street });
                    table.ForeignKey(
                        name: "lock_id_street_fkey",
                        column: x => x.id_street,
                        principalTable: "adress",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    departament_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.login);
                    table.ForeignKey(
                        name: "users_departament_id_fkey",
                        column: x => x.departament_id,
                        principalTable: "departament",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "audit",
                columns: table => new
                {
                    number = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_door = table.Column<int>(type: "integer", nullable: false),
                    id_street = table.Column<int>(type: "integer", nullable: false),
                    login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    closed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("audit_pkey", x => x.number);
                    table.ForeignKey(
                        name: "audit_id_door_id_street_fkey",
                        columns: x => new { x.id_door, x.id_street },
                        principalTable: "lock",
                        principalColumns: new[] { "id", "id_street" });
                    table.ForeignKey(
                        name: "audit_login_fkey",
                        column: x => x.login,
                        principalTable: "users",
                        principalColumn: "login");
                });

            migrationBuilder.CreateIndex(
                name: "IX_audit_id_door_id_street",
                table: "audit",
                columns: new[] { "id_door", "id_street" });

            migrationBuilder.CreateIndex(
                name: "IX_audit_login",
                table: "audit",
                column: "login");

            migrationBuilder.CreateIndex(
                name: "IX_lock_id_street",
                table: "lock",
                column: "id_street");

            migrationBuilder.CreateIndex(
                name: "IX_users_departament_id",
                table: "users",
                column: "departament_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit");

            migrationBuilder.DropTable(
                name: "lock");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "adress");

            migrationBuilder.DropTable(
                name: "departament");
        }
    }
}
