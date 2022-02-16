using System;
using System.Collections;
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
using ChatRoom = Common.ChatRoom;

//using ChatRoom = Common.ChatRoom;

namespace ChatRoom.Tests.Controllers;

public class ChatRoomControllerTests
{
    [Fact]
    public async Task WhenGet_ThenShouldReturnCorrectCollectionType()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetChatRooms().Returns(await Task.FromResult(new List<Common.ChatRoom>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var result = await controller.Get();

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<IEnumerable<Common.ChatRoom>>();
    }

    [Fact]
    public async Task WhenGet_ThenShouldCallServiceForData()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetChatRooms().Returns(await Task.FromResult(new List<Common.ChatRoom>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        await controller.Get();

        // Assert
        await chatRoomService.Received(1).GetChatRooms();
    }

    [Fact]
    public async Task WhenGet_ThenDataFromServiceReturned()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        var chatRooms = new List<Common.ChatRoom> { new() {ChatRoomId = 1, CreateBy = new User {Id = 23}, CreatedById = 23, CreatoinTime = DateTime.UtcNow, Name = "Test"}};
        chatRoomService.GetChatRooms().Returns(await Task.FromResult(chatRooms));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var result = await controller.Get();

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.Value.Should().NotBeNull();
        objectResult!.Value.Should().Be(chatRooms);
    }

    [Fact]
    public async Task GivenNewChatRoom_WhenPut_ThenShouldReturnCorrectCollectionType()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.AddChatRoom(Arg.Any<Common.ChatRoom>()).Returns(await Task.FromResult(new Common.ChatRoom()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var result = await controller.Put(new Common.ChatRoom());

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<Common.ChatRoom>();
    }

    [Fact]
    public async Task GivenExistingChatRoom_WhenPut_ThenShouldReturnCorrectCollectionType()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.UpdateChatRoom(Arg.Any<Common.ChatRoom>()).Returns(await Task.FromResult(new Common.ChatRoom()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var result = await controller.Put(new Common.ChatRoom{ChatRoomId = 2});

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<Common.ChatRoom>();
    }

    [Fact]
    public async Task GivenNewChatRoom_WhenPut_ThenServiceCalledToAddNewChatRoomWithProvidedValue()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.AddChatRoom(Arg.Any<Common.ChatRoom>()).Returns(await Task.FromResult(new Common.ChatRoom()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var chatRoomToAdd = new Common.ChatRoom();
        var result = await controller.Put(chatRoomToAdd);

        // Assert
        await chatRoomService.Received(1).AddChatRoom(Arg.Is(chatRoomToAdd));
    }

    [Fact]
    public async Task GivenExistingChatRoom_WhenPut_ThenServiceCalledToUpdateChatRoomWithProvidedValue()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.UpdateChatRoom(Arg.Any<Common.ChatRoom>()).Returns(await Task.FromResult(new Common.ChatRoom()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var chatRoomToUpdate = new Common.ChatRoom { ChatRoomId = 2 };
        var result = await controller.Put(chatRoomToUpdate);

        // Assert
        await chatRoomService.Received(1).UpdateChatRoom(chatRoomToUpdate);
    }

    [Fact]
    public async Task WhenPutFails_ThenErrorCodeReturned()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService
            .When(x =>x.UpdateChatRoom(Arg.Any<Common.ChatRoom>()))
            .Do(_ => throw new Exception("Failed saving chat room data"));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var chatRoomToUpdate = new Common.ChatRoom { ChatRoomId = 2 };
        var result = await controller.Put(chatRoomToUpdate);

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.Value.Should().NotBeNull();
        objectResult!.Value.Should().Be("Failed saving chat room data");
    }

    [Fact]
    public async Task WHenGetEvent_ThenShouldReturnCorrectEventCollectionType()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetMinuteByMinuteEvents(Arg.Any<int>()).Returns(await Task.FromResult(new List<ChatRoomEvent>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());

        // Act
        var result = await controller.GetMinuteByMinuteEvents(Arg.Any<int>());

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeAssignableTo<IEnumerable<ChatRoomEvent>>();
    }

    [Theory]
    [InlineData(2, Granularities.MinuteByMinute)]
    [InlineData(2, Granularities.Hourly)]
    [InlineData(3, Granularities.MinuteByMinute)]
    [InlineData(3, Granularities.Hourly)]
    public async Task WHenGetEvent_ThenCallChatRoomServiceGetEventsWithCorrectParameters(int chatRoomId, Granularities granularity)
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetMinuteByMinuteEvents(Arg.Is(chatRoomId)).Returns(await Task.FromResult(new List<ChatRoomEvent>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result = await controller.GetMinuteByMinuteEvents(chatRoomId);

        // Assert
        await chatRoomService.Received(1).GetMinuteByMinuteEvents(Arg.Is(chatRoomId));
    }

    [Fact]
    public async Task WHenGetEvent_ThenShouldReturnDataReceivedFromService()
    {
        // Arrange
        const int chatRoomId = 2;
        ;
        var chatRoomService = Substitute.For<IChatRoomService>();
        var events_1= new List<ChatRoomEvent>();
        var events_2 = new List<ChatRoomEvent>
            {new() {Id = 2, Comment = new Comment {Id = 2, CommentString = "Test"}}};
        var events_3 = new List<ChatRoomEvent>
            {new() {Id = 3, Comment = new Comment {Id = 3, CommentString = "Test_3"}}};
        chatRoomService.GetMinuteByMinuteEvents(Arg.Is(chatRoomId))
            .Returns(
                await Task.FromResult(events_1),
                await Task.FromResult(events_2),
                await Task.FromResult(events_3));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result_1 = await controller.GetMinuteByMinuteEvents(chatRoomId);
        var result_2 = await controller.GetMinuteByMinuteEvents(chatRoomId);
        var result_3 = await controller.GetMinuteByMinuteEvents(chatRoomId);

        // Assert
        var objectResult_1 = result_1.Result as ObjectResult;
        objectResult_1!.Value.Should().BeEquivalentTo<IEnumerable<ChatRoomEvent>>(events_1);
        var objectResult_2 = result_2.Result as ObjectResult;
        objectResult_2!.Value.Should().BeEquivalentTo<IEnumerable<ChatRoomEvent>>(events_2);
        var objectResult_3 = result_3.Result as ObjectResult;
        objectResult_3!.Value.Should().BeEquivalentTo<IEnumerable<ChatRoomEvent>>(events_3);
    }

    [Theory]
    [ClassData(typeof(CalculatorTestData))]
    public async Task WHenGetEvent_ThenShouldReturnDataReceivedFromServiceTheory(int chatRoomId, Granularities granularity, List<ChatRoomEvent> events)
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetMinuteByMinuteEvents(Arg.Is(chatRoomId))
            .Returns(events);
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result = await controller.GetMinuteByMinuteEvents(chatRoomId);

        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeEquivalentTo<IEnumerable<ChatRoomEvent>>(events);
    }

    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, Granularities.MinuteByMinute, new List<ChatRoomEvent>() };
            yield return new object[] { 1, Granularities.Hourly, new List<ChatRoomEvent>
                {new() {Id = 2, Comment = new Comment {Id = 2, CommentString = "Test"}}}};
            yield return new object[] { 1, Granularities.Hourly, new List<ChatRoomEvent>
            {
                new() {Id = 2, Comment = new Comment {Id = 2, CommentString = "Test4"}},
                new() {Id = 3, Comment = new Comment {Id = 1, CommentString = "Test23"}}
            }};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}