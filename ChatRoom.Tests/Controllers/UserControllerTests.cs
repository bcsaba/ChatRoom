using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using ChatRoom.Controllers;
using Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace ChatRoom.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task WHenGetUser_THenShouldReturnCorrectUserCollectionType()
    {
        // Arrange
        var userService = Substitute.For<IUserService>();
        userService.GetUsers().Returns(await Task.FromResult(new List<User>()));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());
        // Act
        var result = await controller.Get();

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<IEnumerable<User>>();
    }

    [Fact]
    public async Task WhenGetUser_ThenUserServiceUsedToGetUserList()
    {
        // Arrange
        var userService = Substitute.For<IUserService>();
        userService.GetUsers().Returns(await Task.FromResult(new List<User>()));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());

        // Act
        await controller.Get();

        // Assert
        await userService.Received(1).GetUsers();
    }
}