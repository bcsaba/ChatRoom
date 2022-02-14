using BusinessLogic;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserService userService,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return new OkObjectResult(await _userService.GetUsers());
    }

    [HttpPut]
    public async Task<ActionResult<User>> Put()
    {
        throw new NotImplementedException();
    }
}