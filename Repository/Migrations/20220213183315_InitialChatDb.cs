using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class InitialChatDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_UserBlogId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvent_Blogs_UserBlogId",
                table: "RoomEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvent_ChatRooms_ChatRoomId",
                table: "RoomEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvent_RoomEventTypes_EventTypeRoomEventTypeId",
                table: "RoomEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomEvent",
                table: "RoomEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "RoomEvent",
                newName: "RoomEvents");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "RoomEventTypeId",
                table: "RoomEventTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserBlogId",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Posts",
                newName: "ChatRoomId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserBlogId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameColumn(
                name: "UserBlogId",
                table: "RoomEvents",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EventTypeRoomEventTypeId",
                table: "RoomEvents",
                newName: "TargetUserId");

            migrationBuilder.RenameColumn(
                name: "RoomEventId",
                table: "RoomEvents",
                newName: "EventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvent_UserBlogId",
                table: "RoomEvents",
                newName: "IX_RoomEvents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvent_EventTypeRoomEventTypeId",
                table: "RoomEvents",
                newName: "IX_RoomEvents_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvent_ChatRoomId",
                table: "RoomEvents",
                newName: "IX_RoomEvents_ChatRoomId");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Users",
                newName: "NickNAme");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Users",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ChatRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatoinTime",
                table: "ChatRooms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "RoomEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventTypeId",
                table: "RoomEvents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventTime",
                table: "RoomEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomEvents",
                table: "RoomEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ChatRoomId",
                table: "Posts",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_CreatedById",
                table: "ChatRooms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoomEvents_EventTypeId",
                table: "RoomEvents",
                column: "EventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_CreatedById",
                table: "ChatRooms",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ChatRooms_ChatRoomId",
                table: "Posts",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_ChatRooms_ChatRoomId",
                table: "RoomEvents",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_RoomEventTypes_EventTypeId",
                table: "RoomEvents",
                column: "EventTypeId",
                principalTable: "RoomEventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvents_Users_UserId",
                table: "RoomEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("INSERT INTO public.\"RoomEventTypes\" (\"Id\", \"Name\") VALUES(1, 'enter-the-room');");
            migrationBuilder.Sql("INSERT INTO public.\"RoomEventTypes\" (\"Id\", \"Name\") VALUES(2, 'leave-the-room');");
            migrationBuilder.Sql("INSERT INTO public.\"RoomEventTypes\" (\"Id\", \"Name\") VALUES(3, 'comment');");
            migrationBuilder.Sql("INSERT INTO public.\"RoomEventTypes\" (\"Id\", \"Name\") VALUES(4, 'high-five-another-user');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_CreatedById",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ChatRooms_ChatRoomId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_ChatRooms_ChatRoomId",
                table: "RoomEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_RoomEventTypes_EventTypeId",
                table: "RoomEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_Users_TargetUserId",
                table: "RoomEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomEvents_Users_UserId",
                table: "RoomEvents");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ChatRoomId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_CreatedById",
                table: "ChatRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomEvents",
                table: "RoomEvents");

            migrationBuilder.DropIndex(
                name: "IX_RoomEvents_EventTypeId",
                table: "RoomEvents");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "CreatoinTime",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomEvents");

            migrationBuilder.DropColumn(
                name: "EventTime",
                table: "RoomEvents");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Blogs");

            migrationBuilder.RenameTable(
                name: "RoomEvents",
                newName: "RoomEvent");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomEventTypes",
                newName: "RoomEventTypeId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "UserBlogId");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "Posts",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_UserBlogId");

            migrationBuilder.RenameColumn(
                name: "NickNAme",
                table: "Blogs",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Blogs",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RoomEvent",
                newName: "UserBlogId");

            migrationBuilder.RenameColumn(
                name: "TargetUserId",
                table: "RoomEvent",
                newName: "EventTypeRoomEventTypeId");

            migrationBuilder.RenameColumn(
                name: "EventTypeId",
                table: "RoomEvent",
                newName: "RoomEventId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvents_UserId",
                table: "RoomEvent",
                newName: "IX_RoomEvent_UserBlogId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvents_TargetUserId",
                table: "RoomEvent",
                newName: "IX_RoomEvent_EventTypeRoomEventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomEvents_ChatRoomId",
                table: "RoomEvent",
                newName: "IX_RoomEvent_ChatRoomId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "RoomEvent",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RoomEventId",
                table: "RoomEvent",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomEvent",
                table: "RoomEvent",
                column: "RoomEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_UserBlogId",
                table: "Posts",
                column: "UserBlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvent_Blogs_UserBlogId",
                table: "RoomEvent",
                column: "UserBlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvent_ChatRooms_ChatRoomId",
                table: "RoomEvent",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomEvent_RoomEventTypes_EventTypeRoomEventTypeId",
                table: "RoomEvent",
                column: "EventTypeRoomEventTypeId",
                principalTable: "RoomEventTypes",
                principalColumn: "RoomEventTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
