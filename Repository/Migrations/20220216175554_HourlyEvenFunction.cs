using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class HourlyEvenFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string functionSql = @"
CREATE OR REPLACE FUNCTION getHourlyChatRoomDataFunc(chatRoomId int)
RETURNS Table (
	EventTypeId integer,
	""Name"" text,
	CountType bigint,
	UserInAction bigint,
	TargetUser bigint,
	HourPart timestamp with time zone
) 
language 'plpgsql'
as $a$
begin
	return query
	SELECT 
		e.""EventTypeId""  as ""EventTypeId"", 
		et.""Name"" as ""Name"", 
		count(*) as ""CountType"",
		count(distinct ""UserId"") as ""UserInAction"",
		count(distinct ""TargetUserId"") as ""TargetUser"",
		date_trunc('hour', ""EventTime"") as ""HourPart""
	FROM public.""RoomEvents"" e
	inner join ""RoomEventTypes"" as et on e.""EventTypeId"" = et.""Id""  
	where e.""ChatRoomId"" = chatRoomId
	group by ""HourPart"", e.""EventTypeId"", et.""Name"";
end
$a$
            ";
	migrationBuilder.Sql(functionSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop function getHourlyChatRoomDataFunc;");
        }
    }
}
