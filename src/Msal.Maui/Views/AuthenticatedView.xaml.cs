using Msal.Maui.Authentication;
using Msal.Maui.Services;

namespace Msal.Maui.Views;

public partial class AuthenticatedView : ContentPage
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;

    public AuthenticatedView(IAuthenticationService authenticationService, INavigationService navigationService)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
        InitializeComponent();

        SetGreeting();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await _authenticationService.SignOutAsync();
        await _navigationService.NavigateToAsync<UnauthenticatedView>();
    }

    private void SetGreeting()
    {
        var name = _authenticationService.AuthenticatedUser.Identity?.Name;
        GreetingLabel.Text = $"Hello, {name ?? "user"}!";
    }
}