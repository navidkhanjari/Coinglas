using Microsoft.EntityFrameworkCore.Migrations;

namespace Academy.Data.Migrations
{
    public partial class fix_relation_OrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Courses_ItemId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Subscribes_ItemId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubId",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CourseId",
                table: "OrderDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SubId",
                table: "OrderDetails",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Courses_CourseId",
                table: "OrderDetails",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Subscribes_SubId",
                table: "OrderDetails",
                column: "SubId",
                principalTable: "Subscribes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Courses_CourseId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Subscribes_SubId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_CourseId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_SubId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "SubId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "OrderDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Courses_ItemId",
                table: "OrderDetails",
                column: "ItemId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Subscribes_ItemId",
                table: "OrderDetails",
                column: "ItemId",
                principalTable: "Subscribes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
