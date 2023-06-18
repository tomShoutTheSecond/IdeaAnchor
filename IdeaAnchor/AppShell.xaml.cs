using IdeaAnchor.Pages;
using Microsoft.Maui.Platform;

namespace IdeaAnchor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(IdeaPage), typeof(IdeaPage));
    }
}

