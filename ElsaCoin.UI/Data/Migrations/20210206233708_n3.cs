using Microsoft.EntityFrameworkCore.Migrations;

namespace ElsaCoin.UI.Data.Migrations
{
    public partial class n3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Blockchain_BlockId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_BlockId",
                table: "Transactions",
                newName: "IX_Transactions_BlockId");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Blockchain_BlockId",
                table: "Transactions",
                column: "BlockId",
                principalTable: "Blockchain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Blockchain_BlockId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BlockId",
                table: "Transaction",
                newName: "IX_Transaction_BlockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Blockchain_BlockId",
                table: "Transaction",
                column: "BlockId",
                principalTable: "Blockchain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
