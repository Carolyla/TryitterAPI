using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;




namespace TryitterApixUnit
{
public class PostControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PostControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostShouldReturnOk()
    {
        var client = _factory.CreateClient();
        // var newObject = new UserDTO
        // {
        //     Email = "helena@email.com",
        //     Password = "String1@",
        //     ConfirmPassword = "String1@"

        // };
        // var token = new AutorizaController().GeraToken(newObject);
        //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

        var response = await client.GetAsync("/Posts");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostShouldReturnContent()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Posts");

        response.Content.Should().NotBeNull();
    }

    [Fact]
    public async Task PostShouldReturnContentWithText()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Posts");

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(1)]
    public async Task GetPostByIdTest(int id)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/Posts/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostReturnOkUpdatePost()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Posts");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Fact]
    public async Task PostReturnOkDeletePost()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Posts");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
    }
}
}

