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

namespace ChatRoom.Tests.Controllers;

public class ChatRoomControllerTests
{
    [Fact]
    public async Task WHenGetEvent_ThenShouldReturnCorrectEventCollectionType()
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetEvents(Arg.Any<int>(), Arg.Any<Granularities>()).Returns(await Task.FromResult(new List<ChatRoomEvent>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result = await controller.GetEvents(Arg.Any<int>(), Arg.Any<Granularities>());

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
        chatRoomService.GetEvents(Arg.Is(chatRoomId), Arg.Is(granularity)).Returns(await Task.FromResult(new List<ChatRoomEvent>()));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result = await controller.GetEvents(chatRoomId, granularity);

        // Assert
        await chatRoomService.Received(1).GetEvents(Arg.Is(chatRoomId), Arg.Is(granularity));
    }

    [Fact]
    public async Task WHenGetEvent_ThenShouldReturnDatarEceivedFromService()
    {
        // Arrange
        const int chatRoomId = 2;
        const Granularities granularity = Granularities.MinuteByMinute;
        ;
        var chatRoomService = Substitute.For<IChatRoomService>();
        var events_1= new List<ChatRoomEvent>();
        var events_2 = new List<ChatRoomEvent>
            {new() {Id = 2, Comment = new Comment {Id = 2, CommentString = "Test"}}};
        var events_3 = new List<ChatRoomEvent>
            {new() {Id = 3, Comment = new Comment {Id = 3, CommentString = "Test_3"}}};
        chatRoomService.GetEvents(Arg.Is(chatRoomId), Arg.Is(granularity))
            .Returns(
                await Task.FromResult(events_1),
                await Task.FromResult(events_2),
                await Task.FromResult(events_3));
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result_1 = await controller.GetEvents(chatRoomId, granularity);
        var result_2 = await controller.GetEvents(chatRoomId, granularity);
        var result_3 = await controller.GetEvents(chatRoomId, granularity);

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
    public async Task WHenGetEvent_ThenShouldReturnDatarReceivedFromServiceTheory(int chatRoomId, Granularities granularity, List<ChatRoomEvent> events)
    {
        // Arrange
        var chatRoomService = Substitute.For<IChatRoomService>();
        chatRoomService.GetEvents(Arg.Is(chatRoomId), Arg.Is(granularity))
            .Returns(events);
        var controller = new ChatRoomController(chatRoomService, Substitute.For<ILogger<ChatRoomController>>());
        // Act
        var result = await controller.GetEvents(chatRoomId, granularity);

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