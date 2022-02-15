using System.Net;
using BusinessLogic;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatRoomController : ControllerBase
{
    private readonly IChatRoomService _chatRoomService;
    private readonly ILogger<ChatRoomController> _logger;

    public ChatRoomController(
        IChatRoomService chatRoomService,
        ILogger<ChatRoomController> logger)
    {
        _chatRoomService = chatRoomService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Common.ChatRoom>>> Get()
    {
        return new OkObjectResult(await _chatRoomService.GetChatRooms());
    }

    [HttpPut]
    public async Task<ActionResult<Common.ChatRoom>> Put([FromBody] Common.ChatRoom chatRoom)
    {
        try
        {
            if (chatRoom.ChatRoomId != null || chatRoom.ChatRoomId > 0)
                return new OkObjectResult(await _chatRoomService.UpdateChatRoom(chatRoom));

            return new OkObjectResult(await _chatRoomService.AddChatRoom(chatRoom));
        }
        catch (Exception e)
        {
            _logger.LogError("Failed saving chat room data");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Route("{chatRoomId}/user/{userId}/eventtype/{eventTypeId}")]
    public async Task<ActionResult<ChatRoomEvent>> ChatRoomAction(
        [FromRoute] int userId,
        [FromRoute] int chatRoomId,
        [FromRoute] int eventTypeId,
        [FromBody] ChatRoomEventInfo eventInfo)
    {
        return new OkObjectResult(await _chatRoomService.AddAction(chatRoomId, userId, eventTypeId, eventInfo));
    }

    [HttpGet]
    [Route("{chatRoomId}/granularity/{granularity}")]
    public async Task<ActionResult<IEnumerable<ChatRoomEvents>>> GetEvents(
        [FromRoute] int chatRoomId,
        [FromRoute] Granularities granularity)
    {
        return new OkObjectResult(await _chatRoomService.GetEvents(chatRoomId, granularity));
    }
}