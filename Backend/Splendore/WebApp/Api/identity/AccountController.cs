﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using DAL.EF.App;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WebApp.Api.identity;

/// <inheritdoc />
[ApiController]
[ApiVersion( "1.0" )]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly Random _rnd = new();


    /// <inheritdoc />
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        IConfiguration configuration, ApplicationDbContext context, ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
        _logger = logger;
    }
    
    /// <summary>
    /// Register new user to the system.
    /// </summary>
    /// <param name="registrationData">User info.</param>
    /// <param name="expiresInSeconds">Override default value (optional).</param>
    /// <returns>JWTResponse</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JWTResponse>> Register([FromBody] Register registrationData, 
        [FromQuery] int expiresInSeconds)
    {
        // if user is already registered
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"User with email {registrationData.Email} is already registered"
            });
        }

        // register user
        var refreshToken = new AppRefreshToken();
        appUser = new AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            FirstName = registrationData.Firstname,
            LastName = registrationData.Lastname,
            AppRefreshTokens = new List<AppRefreshToken>() {refreshToken}
        };
        refreshToken.AppUser = appUser;

        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = result.Errors.First().Description
            });
        }

        // -------------------------------------------------------------------------------------------------------------
        // save into claims also the user full name
        result = await _userManager.AddClaimsAsync(appUser, new List<Claim>()
        {
            new(ClaimTypes.GivenName, appUser.FirstName),
            new(ClaimTypes.Surname, appUser.LastName)
        });

        if (!result.Succeeded)
        {
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = result.Errors.First().Description
            });
        }

        // get full user from system with fixed data (maybe there is something generated by identity that we might need)
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", registrationData.Email);
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"User with email {registrationData.Email} is not found after registration"
            });
        }

        // -------------------------------------------------------------------------------------------------------------

        // generate jwt
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:Key")!,
            _configuration.GetValue<string>("JWT:Issuer")!,
            _configuration.GetValue<string>("JWT:Audience")!,
            _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );

        // return result
        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(res);
    }

    /// <summary>
    /// Login existing user.
    /// </summary>
    /// <param name="loginData">User info</param>
    /// <returns>JWTResponse</returns>
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> LogIn([FromBody] Login loginData)
    {
        // verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));

            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }

        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }

        appUser.AppRefreshTokens = await _context
            .Entry(appUser)
            .Collection(a => a.AppRefreshTokens!)
            .Query()
            .Where(t => t.AppUserId == appUser.Id)
            .ToListAsync();

        // remove expired tokens
        foreach (var userRefreshToken in appUser.AppRefreshTokens)
        {
            if (
                userRefreshToken.ExpirationDT < DateTime.UtcNow &&
                (
                    userRefreshToken.PreviousExpirationDT == null ||
                    userRefreshToken.PreviousExpirationDT < DateTime.UtcNow
                )
            )
            {
                _context.AppRefreshTokens.Remove(userRefreshToken);
            }
        }

        var refreshToken = new AppRefreshToken
        {
            AppUserId = appUser.Id
        };
        _context.AppRefreshTokens.Add(refreshToken);

        // generate jwt
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"]!,
            _configuration["JWT:Issuer"]!,
            _configuration["JWT:Audience"]!,
            _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );

        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };

        return Ok(res);
    }

    /// <summary>
    /// Refresh user jwt token.
    /// </summary>
    /// <param name="refreshTokenModel">User current refresh token</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> RefreshToken(
        [FromBody] RefreshTokenModel refreshTokenModel)
    {
        JwtSecurityToken jwtToken;

        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No token"
                });
            }
        }
        catch (Exception e)
        {
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"Cant parse the token, {e.Message}"
            });
        }

        if (!IdentityHelpers.ValidateToken(refreshTokenModel.Jwt,
                _configuration["JWT:Key"]!,
                _configuration["JWT:Issuer"]!,
                _configuration["JWT:Audience"]!))
        {
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = "JWT validation fail"
            });
        }

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = "No email in jwt"
            });
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = $"User with email {userEmail} not found"
            });
        }

        var tokens = await _context.AppRefreshTokens.Where(rt => rt.AppUserId == appUser.Id).ToListAsync();
        
        foreach (var token in tokens)
        {
            Console.WriteLine("refreshToken: " + System.Text.Json.JsonSerializer.Serialize(token.RefreshToken));
        }
        
        
        // load and compare refresh tokens
        await _context.Entry(appUser).Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == refreshTokenModel.RefreshToken && x.ExpirationDT > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == refreshTokenModel.RefreshToken &&
                 x.PreviousExpirationDT > DateTime.UtcNow)
            )
            .ToListAsync();

        if (appUser.AppRefreshTokens == null || appUser.AppRefreshTokens.Count == 0)
        {
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = $"RefreshTokens collection is null or empty - {appUser.AppRefreshTokens?.Count}"
            });
        }

        if (appUser.AppRefreshTokens.Count != 1)
        {
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "More than one valid refresh token found"
            });
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }

        // generate jwt
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"]!,
            _configuration["JWT:Issuer"]!,
            _configuration["JWT:Audience"]!,
            _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );

        // make new refresh token, keep old one still valid for some time
        var refreshToken = appUser.AppRefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpirationDT = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.ExpirationDT = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }

        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };

        return Ok(res);
    }
    
    /// <summary>
    /// Log user out of system.
    /// </summary>
    /// <param name="logout">Logout info.</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Logout(
        [FromBody] Logout logout)
    {
        // delete the refresh token - so user is kicked out after jwt expiration
        // We do not invalidate the jwt - that would require pipeline modification and checking against db on every request
        // so client can actually continue to use the jwt until it expires (keep the jwt expiration time short ~1 min)

        var userId = User.GetUserId();

        var appUser = await _context.Users
            .Where(u => u.Id == userId)
            .SingleOrDefaultAsync();
        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }
        
        await _context.Entry(appUser)
            .Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == logout.RefreshToken) ||
                (x.PreviousRefreshToken == logout.RefreshToken)
            )
            .ToListAsync();

        foreach (var appRefreshToken in appUser.AppRefreshTokens!)
        {
            _context.AppRefreshTokens.Remove(appRefreshToken);
        }

        var deleteCount = await _context.SaveChangesAsync();

        return Ok(new {TokenDeleteCount = deleteCount});
    }

}