using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkAPI.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pay_People_PersonID",
                table: "Pay");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Pay_PersonID",
                table: "Pay");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Pay");

            migrationBuilder.AddColumn<int>(
                name: "PersonalID",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidAt",
                table: "Pay",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pay",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pay");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidAt",
                table: "Pay",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Pay",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeopleID = table.Column<int>(type: "int", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_People_PeopleID",
                        column: x => x.PeopleID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pay_PersonID",
                table: "Pay",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PeopleID",
                table: "Vehicles",
                column: "PeopleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pay_People_PersonID",
                table: "Pay",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
