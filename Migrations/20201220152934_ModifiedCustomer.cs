using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class ModifiedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "customer_AddressShip1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "customer_AddressShip2",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_PaymentDate",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "StatusOrder",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customer_PhoneNumber",
                table: "Customers",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "customer_AddressShip",
                table: "Customers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "categories_Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "brand_Name",
                table: "Brands",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 1,
                columns: new[] { "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[] { "Nội thành", 20000m });

            migrationBuilder.UpdateData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 6,
                columns: new[] { "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[] { "Ngoại thành", 50000m });

            migrationBuilder.InsertData(
                table: "DeliveryCosts",
                columns: new[] { "deliverycost_ID", "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[] { 7, "Miền Bắc", 100000m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 1,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Chiết khấu mặc định", 0 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 2,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Giảm 10% cho sản phẩm", 10 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 3,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Giảm 15% đơn hàng", 15 });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "promotion_ID", "promotion_Name", "promotion_Percent" },
                values: new object[] { 4, "Giảm 50% cho mỗi đơn hàng", 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "StatusOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "customer_AddressShip",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_PaymentDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customer_PhoneNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "customer_AddressShip1",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customer_AddressShip2",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "categories_Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "brand_Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 1,
                columns: new[] { "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[] { "Quận 1", 15000m });

            migrationBuilder.UpdateData(
                table: "DeliveryCosts",
                keyColumn: "deliverycost_ID",
                keyValue: 6,
                columns: new[] { "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[] { "Miền Bắc", 100000m });

            migrationBuilder.InsertData(
                table: "DeliveryCosts",
                columns: new[] { "deliverycost_ID", "deliverycost_AreaName", "deliverycost_Cost" },
                values: new object[,]
                {
                    { 2, "Quận 2", 15000m },
                    { 3, "Quận 3", 10000m },
                    { 4, "Quận 4", 30000m },
                    { 5, "Ngoại thành", 50000m }
                });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 1,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Giảm 10% cho sản phẩm", 10 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 2,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Giảm 15% đơn hàng", 15 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "promotion_ID",
                keyValue: 3,
                columns: new[] { "promotion_Name", "promotion_Percent" },
                values: new object[] { "Giảm 50% cho mỗi đơn hàng", 50 });
        }
    }
}
