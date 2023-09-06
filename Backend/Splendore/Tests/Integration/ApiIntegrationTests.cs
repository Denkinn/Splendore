using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AngleSharp.Html.Dom;
using Public.DTO.v1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Public.DTO.v1.Identity;
using Tests.Helpers;
using Xunit.Abstractions;

namespace Tests.Integration;

public class ApiIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly Guid stylistUserId = Guid.NewGuid();
    private readonly Guid appUserId = Guid.NewGuid();

    private readonly JsonSerializerOptions camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ApiIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "GET - get list of salons")]
    public async Task SalonsPageTest()
    {
        // Arrange
        
        // Act
        await CreateSalonTest();
        var response = await _client.GetAsync("/api/v1/Salons");

        // Assert
        response.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
    
    [Fact(DisplayName = "POST - create new salon")]
    public async Task<Guid> CreateSalonTest()
    {
        // Arrange
        var salonId = Guid.NewGuid();
        var salonData = new
        {
            Id = salonId,
            Name = "Test salon",
            Address = "Test address",
            Email = "Test email",
            PhoneNumber = "Test phone number"
        };

        // Act
        var response = await _client.PostAsync("/api/v1/Salons", JsonContent.Create(salonData));

        // Assert
        response.EnsureSuccessStatusCode();

        return salonId;
    }
    
    [Fact(DisplayName = "POST - add service to salon")]
    private async Task<Guid> CreateSalonServiceTest()
    {
        // Service Type
        // Arrange
        var serviceTypeId = Guid.NewGuid();
        var serviceType = JsonContent.Create(new
        {
            Id = serviceTypeId,
            Name = "Hairstyle"
        });
        
        // Act
        var serviceTypeResponse = await _client.PostAsync("/api/v1/ServiceTypes", serviceType);
        
        //Assert
        serviceTypeResponse.EnsureSuccessStatusCode();
        
        // ======================================================================
        // Service
        // Arrange
        var serviceId = Guid.NewGuid();
        var service = JsonContent.Create(new
        {
            Id = serviceId,
            Name = "Basic haircut",
            ServiceTypeId = serviceTypeId
        }); 
        
        // Act
        var serviceResponse = await _client.PostAsync("/api/v1/Services", service);

        // Assert
        serviceResponse.EnsureSuccessStatusCode();
        
        // ======================================================================
        // Salon service
        var salonServiceId = Guid.NewGuid();
        var salonId = await CreateSalonTest();

        var salonService = JsonContent.Create(new
        {
            Id = salonServiceId,
            Price = 15,
            Time = 25,
            SalonId = salonId,
            ServiceId = serviceId
        });
        
        // Act
        var salonServiceResponse = await _client.PostAsync("/api/v1/SalonServices", salonService);
        
        // Assert
        salonServiceResponse.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await salonServiceResponse.Content.ReadAsStringAsync());
        
        return salonServiceId;
    }

    [Fact(DisplayName = "POST - register user and get JWT")]
    private async Task<string> RegisterUserTest()
    {
        // Arrange
        var URL = "/api/v1/identity/account/register";

        var registerData = new
        {
            Id = appUserId,
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
        
        _testOutputHelper.WriteLine(responseContent);

        return jwtResponse.JWT;
    }
    
    [Fact(DisplayName = "POST - create new stylist")]
    private async Task<Guid> CreateStylistTest()
    {
        // Arrange
        var stylistId = Guid.NewGuid();
        var salonId = await CreateSalonTest();
        var stylistData = new
        {
            Id = stylistId,
            Name = "Elizabeth",
            PhoneNumber = "+372 57382624",
            SalonId = salonId,
            AppUserId = stylistUserId
        };

        // Act
        var response = await _client.PostAsync("/api/v1/Stylists", JsonContent.Create(stylistData));

        // Assert
        response.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

        return stylistId;
    }

    [Fact(DisplayName = "POST - add schedule to stylist")]
    private async Task<Schedule?> CreateScheduleTest()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var stylistId = await CreateStylistTest();
        var schedule = JsonContent.Create(new
        {
            Id = scheduleId,
            Date = DateTime.Now.AddDays(1),
            IsBusy = false,
            StylistId = stylistId
        });
        
        // Act
        var response = await _client.PostAsync("/api/v1/Schedules", schedule);
        
        // Assert
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        
        _testOutputHelper.WriteLine(responseContent);

        var createdSchedule = JsonSerializer.Deserialize<Schedule>(responseContent, camelCaseJsonSerializerOptions);
        
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(createdSchedule));

        return createdSchedule;
    }

    [Fact(DisplayName = "POST - create new appointment")]
    private async Task CreateAppointmentTest()
    {
        // Appointment status
        // Arrange
        var appointmentStatusId = Guid.NewGuid();
        var appointmentStatus = JsonContent.Create(new
        {
            Id = appointmentStatusId,
            Name = "Active"
        });
        
        // Act
        var appointmentStatusResponse = await _client.PostAsync("/api/v1/AppointmentStatuses", appointmentStatus);
        
        // Assert
        appointmentStatusResponse.EnsureSuccessStatusCode();
        
        // ======================================================================
        // Payment method
        // Arrange
        var paymentMethodId = Guid.NewGuid();
        var paymentMethod = JsonContent.Create(new
        {
            Id = paymentMethodId,
            Name = "Cash"
        });
        
        // Act
        var paymentMethodResponse = await _client.PostAsync("/api/v1/PaymentMethods", paymentMethod);
        
        // Assert
        paymentMethodResponse.EnsureSuccessStatusCode();
        
        // ======================================================================
        // Appointment
        // Arrange
        var schedule = await CreateScheduleTest();
        var stylistId = await CreateStylistTest();
        var appointment = JsonContent.Create(new
        {
            Date = schedule!.Date,
            TotalPrice = 15,
            StylistId = stylistId,
            AppointmentStatusId = appointmentStatusId,
            ScheduleId = schedule.Id,
            PaymentMethodId = paymentMethodId,
            AppUserId = appUserId
        });
        
        // Act
        var appointmentResponseUnauthorized = await _client.PostAsync("/api/v1/Appointments", appointment);
        
        // Assert
        Assert.True(appointmentResponseUnauthorized.StatusCode == HttpStatusCode.Unauthorized);
        
        // Arrange
        var jwtToken = await RegisterUserTest();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        
        // Act
        var appointmentResponseAuthorized = await _client.PostAsync("/api/v1/Appointments", appointment);
        
        // Assert
        appointmentResponseAuthorized.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "POST - leave review to salon")]
    private async Task CreateReviewTest()
    {
        // Arrange
        var salonId = await CreateSalonTest();
        var review = JsonContent.Create(new
        {
            SalonId = salonId,
            Rating = 5,
            Commentary = "Good salon and enjoyable services"
        });
        
        // Act
        var response = await _client.PostAsync("/api/v1/Reviews", review);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
