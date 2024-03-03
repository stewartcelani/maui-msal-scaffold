using Msal.Maui.Views;

namespace Msal.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute(nameof(UnauthenticatedView).ToLower(), typeof(UnauthenticatedView));
        Routing.RegisterRoute(nameof(AuthenticatedView).ToLower(), typeof(AuthenticatedView));
        InitializeComponent();
    }
}