using Microsoft.EntityFrameworkCore.Migrations;

namespace Academy.Data.Migrations
{
    public partial class newAddOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Subscribes_SubId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "SubId",
                table: "OrderDetails",
                newName: "SubscribeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_SubId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_SubscribeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Subscribes_SubscribeId",
                table: "OrderDetails",
                column: "SubscribeId",
                principalTable: "Subscribes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Subscribes_SubscribeId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "SubscribeId",
                table: "OrderDetails",
                newName: "SubId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_SubscribeId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Subscribes_SubId",
                table: "OrderDetails",
                column: "SubId",
                principalTable: "Subscribes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
