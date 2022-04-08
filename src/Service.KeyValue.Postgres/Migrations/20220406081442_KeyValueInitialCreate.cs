using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.KeyValue.Postgres.Migrations
{
    public partial class KeyValueInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "education");

            migrationBuilder.CreateTable(
                name: "keyvalue",
                schema: "education",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyvalue", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_keyvalue_UserId_Key",
                schema: "education",
                table: "keyvalue",
                columns: new[] { "UserId", "Key" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "keyvalue",
                schema: "education");
        }
    }
}
