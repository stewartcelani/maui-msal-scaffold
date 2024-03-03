using Microsoft.AspNetCore.Components.Authorization;

namespace Msal.Maui.Authentication;

// This is a custom AuthenticationStateProvider that will be used to provide the current user's authentication state to a Blazor Hybrid WebView.
public class HybridAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationService _authenticationService;

    public HybridAuthenticationStateProvider(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var task = Task.FromResult(new AuthenticationState(_authenticationService.AuthenticatedUser));
        return task;
    }
}