using IdeaAnchor.Pages;
using Microsoft.Maui.Platform;

namespace IdeaAnchor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(IdeaPage), typeof(IdeaPage));
        Routing.RegisterRoute(nameof(IdeasListPage), typeof(IdeasListPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}

