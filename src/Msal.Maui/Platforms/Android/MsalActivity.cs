using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Msal.Maui;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msalc8ccdf87-b572-4ba6-4a36-b034876b1bd2")]
public class MsalActivity : BrowserTabActivity
{
}