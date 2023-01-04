using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using System.Net.Http.Headers;
using System.Net;
using TryitterApi.Models;

namespace TryitterApixUnit
{
public class StudentControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StudentControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldReturnOk()
    {
        var client = _factory.CreateClient();


        var response = await client.GetAsync("/Students");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.Content.ReadAsStringAsync().Result.Should().Contain("name");
        response.Content.ReadAsStringAsync().Result.Should().Contain("email");
        
    }

    [Theory]
    [InlineData(2)]
    public async Task ShouldReturnOkWithId(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync($"/Students/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    // [Fact]
    // public async Task ShoulReturnOkCreateStudent()
    // {
    //     var client = _factory.CreateClient();

    //     var student = new Student
    //     {
    //         Name = "Teste",
    //         Email = "email@email.com",
    //         Password = "secreta"
    //     };

    //     var response = await client.PostAsJsonAsync("/Students", student);

    //     response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    //     response.Content.ReadAsStringAsync().Result.Should().Contain("name");
    //     response.Content.ReadAsStringAsync().Result.Should().Contain("email");
       
    // }

    
    [Theory]
    [InlineData(1)]
    public async Task ShouldReturnOkDeleteStudent(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync($"/Students/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(0)]
    public async Task ShouldReturnNotFoundDeleteStudent(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync($"/Students/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Theory]
    [InlineData(0)]
    public async Task ShouldReturnNotFoundDeleteStudentWithId(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync($"/Students/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

}

