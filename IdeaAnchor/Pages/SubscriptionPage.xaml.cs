using IdeaAnchor.ViewModels;

namespace IdeaAnchor.Pages;

public partial class SubscriptionPage : ContentPage
{
    private SubscriptionViewModel _vm => BindingContext as SubscriptionViewModel;

    public SubscriptionPage(SubscriptionViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}
