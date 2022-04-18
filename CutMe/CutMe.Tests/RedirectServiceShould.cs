using System;
using System.Threading.Tasks;
using CutMe.Communication;
using CutMe.Exceptions;
using CutMe.Models;
using CutMe.Services;
using CutMe.Storage.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CutMe.Tests;

public class RedirectServiceShould
{
    private readonly IRedirectService _redirectService;
    private const string Shortcut = "111";
    private const string FullUrl = "https://www.google.com/";

    public RedirectServiceShould()
    {
        var redirectRepositoryMock = new Mock<IRedirectRepository>();
        redirectRepositoryMock.Setup(m => m.GetFullUrlAsync(Shortcut))
            .ReturnsAsync(GetRedirectInformation());

        _redirectService = new RedirectService(redirectRepositoryMock.Object);
    }

    [Fact]
    public async Task GetFullUrlAsync_ReturnCorrectFullUrl_When_Shortcut_Is_Correct()
    {
        //Arrange
        
        //Act
        var fullUrl = await _redirectService.GetFullUrlAsync("111");

        //Assert
        fullUrl.Should().Be(FullUrl);
    }
    
    [Fact]
    public async Task GetFullUrlAsync_ThrowResourceNotFound_When_Shortcut_Not_Exist()
    {
        //Arrange
        
        //Act
        Func<Task> act = async () => await _redirectService.GetFullUrlAsync("1121");

        //Assert
        await act.Should().ThrowAsync<ResourceNotFoundException>();
    }
    
    [Fact]
    public async Task GetFullUrlAsync_ThrowArgumentException_When_Shortcut_IsEmpty()
    {
        //Arrange
        
        //Act
        Func<Task> act = async () => await _redirectService.GetFullUrlAsync(string.Empty);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task SetRedirectUrlAsync_ThrowArgumentException_When_Shortcut_IsEmpty()
    {
        //Arrange
        var setRedirectRequest = new SetRedirectRequest
        {
            Shortcut = string.Empty,
            FullUrl = "https://localhost:7078/swagger/index.html"
        };
        
        //Act
        Func<Task> act = async () => await _redirectService.SetRedirectUrlAsync(setRedirectRequest);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task SetRedirectUrlAsync_ThrowConflictException_When_Shortcut_Exist()
    {
        //Arrange
        var setRedirectRequest = new SetRedirectRequest
        {
            Shortcut = Shortcut,
            FullUrl = "https://localhost:7078/swagger/index.html"
        };
        
        //Act
        Func<Task> act = async () => await _redirectService.SetRedirectUrlAsync(setRedirectRequest);

        //Assert
        await act.Should().ThrowAsync<ConflictException>();
    }
    
    private static RedirectInformation? GetRedirectInformation() => new RedirectInformation
    {
        Shortcut = Shortcut,
        FullUrl = FullUrl
    };
}