using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1.Identity;
using Xunit.Abstractions;

namespace Tests.Integration.api.identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    
    private readonly JsonSerializerOptions camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    
    public IdentityIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact(DisplayName = "POST - register new user")]
    public async Task RegisterNewUserTest()
    {
        // Arrange
        var URL = "/api/v1/identity/account/register";

        var registerData = new
        {
            Email = "test@test.ee",
            Password = "Foo.bar1",
            Firstname = "Test first",
            Lastname = "Test last"
        };
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(URL, data);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent, camelCaseJsonSerializerOptions);
        Assert.NotNull(jwtResponse);
        
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(jwtResponse));

    }
    
    private async Task<string> RegisterNewUser(string email, string password, string firstname, string lastname)
    {
        var URL = $"/api/v1/identity/account/register";

        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname,
        };

        var data = JsonContent.Create(registerData);
        // Act
        var response = await _client.PostAsync(URL, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
        return responseContent;
    }

    
    [Fact(DisplayName = "POST - login user")]
    public async Task LoginUser()
    {
        const string email = "login@test.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";

        // Arrange
        await RegisterNewUser(email, password, firstname, lastname);
        
        var URL = "/api/v1/identity/account/login";

        var loginData = new
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);

        // Act
        var response = await _client.PostAsync(URL, data);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
    }
   
    
    [Fact(DisplayName = "POST - JWT expired")]
    public async Task JWTExpired()
    {
        // Arrange

        // Act
        
        // Assert

    }
    
    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task JWTRenewal()
    {
        // Arrange

        // Act
        
        // Assert

    }
    
    [Fact(DisplayName = "POST - JWT logout")]
    public async Task JWTLogout()
    {
        // Arrange

        // Act
        
        // Assert

    }


}