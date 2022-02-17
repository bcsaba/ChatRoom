using System;
using System.Collections.Generic;
using System.Net;
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

    [Fact]
    public async Task WHenPutUser_THenShouldReturnCorrectUserCollectionType()
    {
        // Arrange
        User testUser = new User() {FirstName = "Joe", LastName = "Test", NickName = "TestJ"};
        var userService = Substitute.For<IUserService>();
        userService.AddUser(Arg.Any<User>()).Returns(await Task.FromResult(testUser));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());
        // Act
        var result = await controller.Put(testUser);

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<User>();
    }

    [Fact]
    public async Task WHenPutUserAndHasNoId_THenCallUSerServicePutUser()
    {
        // Arrange
        User testUser = new User() { FirstName = "Joe", LastName = "Test", NickName = "TestJ" };
        var userService = Substitute.For<IUserService>();
        userService.AddUser(Arg.Any<User>()).Returns(await Task.FromResult(testUser));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());
        // Act
        var result = await controller.Put(testUser);

        // Assert
        await userService.Received(1).AddUser(Arg.Any<User>());
    }

    [Fact]
    public async Task WHenPutUserAndHasId_THenCallUSerServiceUpdateUser()
    {
        // Arrange
        User testUser = new User() { Id = 1, FirstName = "Joe", LastName = "Test", NickName = "TestJ" };
        var userService = Substitute.For<IUserService>();
        userService.UpdateUser(Arg.Any<User>()).Returns(await Task.FromResult(testUser));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());
        // Act
        var result = await controller.Put(testUser);

        // Assert
        await userService.Received(1).UpdateUser(Arg.Any<User>());
    }

    [Fact]
    public async Task WHenPutUserFails_THenInternalServerErrorReturned()
    {
        // Arrange
        User testUser = new User() { Id = 1, FirstName = "Joe", LastName = "Test", NickName = "TestJ" };
        var userService = Substitute.For<IUserService>();
        userService.When(x => x.UpdateUser(Arg.Any<User>()))
            .Do(x => throw new Exception("Update user data failed"));
        var controller = new UserController(userService, Substitute.For<ILogger<UserController>>());
        // Act
        var result = await controller.Put(testUser);

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        objectResult.Value.Should().NotBeNull();
        objectResult.Value.Should().Be("Update user data failed");
    }
}