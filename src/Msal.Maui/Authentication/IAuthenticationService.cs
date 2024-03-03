using System.Security.Claims;
using Microsoft.Identity.Client;

namespace Msal.Maui.Authentication;

public interface IAuthenticationService
{
    ClaimsPrincipal AuthenticatedUser { get; }
    Task<AuthenticationResult> AuthenticateAsync();
    Task<ClaimsPrincipal?> ValidateTokenAsync(string idToken);
    Task<bool> SignInAsync();
    Task<bool> SignOutAsync();
}