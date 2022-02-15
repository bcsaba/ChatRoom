using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class ForeignKeyUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "PostingUser");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_PostingUser");

            migrationBuilder.RenameColumn(
                name: "CreatoinTime",
                table: "ChatRooms",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "ChatRooms",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "TargetUserId",
                table: "RoomEvents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_PostingUser",
                table: "Posts",
                column: "PostingUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_PostingUser",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents");

            migrationBuilder.RenameColumn(
                name: "PostingUser",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_PostingUser",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "ChatRooms",
                newName: "CreatoinTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ChatRooms",
                newName: "ChatRoomId");

            migrationBuilder.AlterColumn<int>(
                name: "TargetUserId",
                table: "RoomEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
