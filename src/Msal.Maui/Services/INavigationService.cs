namespace Msal.Maui.Services;

public interface INavigationService
{
    Task NavigateToAsync<TPage>() where TPage : Page;
}