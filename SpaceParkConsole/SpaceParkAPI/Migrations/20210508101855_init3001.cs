using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkAPI.Migrations
{
    public partial class init3001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpacePortID",
                table: "Pay",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpacePorts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slots = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacePorts", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pay_SpacePortID",
                table: "Pay",
                column: "SpacePortID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pay_SpacePorts_SpacePortID",
                table: "Pay",
                column: "SpacePortID",
                principalTable: "SpacePorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pay_SpacePorts_SpacePortID",
                table: "Pay");

            migrationBuilder.DropTable(
                name: "SpacePorts");

            migrationBuilder.DropIndex(
                name: "IX_Pay_SpacePortID",
                table: "Pay");

            migrationBuilder.DropColumn(
                name: "SpacePortID",
                table: "Pay");
        }
    }
}
