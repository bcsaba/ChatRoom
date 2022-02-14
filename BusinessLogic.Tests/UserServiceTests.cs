using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core;
using Repository;
using Xunit;

namespace BusinessLogic.Tests;

public static class DbContextMock
{
    public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var dbSet = Substitute.For<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Provider.Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Expression.Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().ElementType.Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().GetEnumerator().Returns(queryable.GetEnumerator());
        dbSet.When(x => x.Add(Arg.Any<T>())).Do((s) => sourceList.Add(s.Arg<T>()));
        return dbSet;
    }
}

public class UserServiceTests
{
    private readonly IBloggingContext mockContext;

    public UserServiceTests()
    {
        var data = new List<User>
        {
            new User { Id = 1, NickNAme = "TestJ_1", FirstName = "Jack_1", LastName = "Test_1"},
            new User { Id = 1, NickNAme = "TestJ_2", FirstName = "Jack_2", LastName = "Test_2"},
            new User { Id = 1, NickNAme = "TestJ_3", FirstName = "Jack_3", LastName = "Test_3"},
        }.AsQueryable();

        var mockSet = Substitute.For<DbSet<User>, IQueryable<User>>();
        // it's the next four lines I don't get to work
        ((IQueryable<User>)mockSet).Provider.Returns(data.Provider);
        ((IQueryable<User>)mockSet).Expression.Returns(data.Expression);
        ((IQueryable<User>)mockSet).ElementType.Returns(data.ElementType);
        ((IQueryable<User>)mockSet).GetEnumerator().Returns(data.GetEnumerator());

        var options = new DbContextOptionsBuilder<BloggingContext>()
            .Options;
        mockContext = Substitute.For<BloggingContext>(options);
        var tmp = mockContext.Users.Returns(mockSet);
    }

    [Fact(Skip = "Need to fix DbSet substitute")]
    public async Task WhenGetUsers_ThenCorrectUserCollectionTypeReturns()
    {
        // Arrange
        var service = new UserService(mockContext);
        // Act
        var result = await service.GetUsers();

        // Assert
        result.Should().BeAssignableTo<IEnumerable<Common.User>>();
    }

    [Fact(Skip = "Need to fix DbSet substitute")]
    public async Task WhenGetUsers_ThenGetUsersFromRepository()
    {
        // Arrange
        var dbContext = mockContext;
        //dbContext.Users.Returns(Substitute.For<DbSet<User>>());
        var service = new UserService(Substitute.For<IBloggingContext>());
        // Act
        var result = await service.GetUsers();

        // Assert
        var dbSet = dbContext.Received(1).Users;
    }
}