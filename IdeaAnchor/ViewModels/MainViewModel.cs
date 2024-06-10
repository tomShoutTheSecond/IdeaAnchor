using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using IdeaAnchor.Pages;
using IdeaAnchor.Helper;

namespace IdeaAnchor.ViewModels
{
	public class MainViewModel : BindableObject
	{
        private bool _isMyIdeasSelected;
        private bool _isNewIdeaSelected;
        private bool _isProSubscriptionActive;

        public bool IsMyIdeasSelected
        {
            get => _isMyIdeasSelected;
            set
            {
                _isMyIdeasSelected = value;

                OnPropertyChanged(nameof(IsMyIdeasSelected));
                OnPropertyChanged(nameof(MyIdeasButtonBackgroundColor));
            }
        }

        public bool IsNewIdeaSelected
        {
            get => _isNewIdeaSelected;
            set
            {
                _isNewIdeaSelected = value;

                OnPropertyChanged(nameof(IsNewIdeaSelected));
                OnPropertyChanged(nameof(CreateIdeaButtonBackgroundColor));
            }
        }

        public bool IsSubscriptionSelected
        {
            get => _isProSubscriptionActive;
            set
            {
                _isProSubscriptionActive = value;

                OnPropertyChanged(nameof(IsSubscriptionSelected));
                OnPropertyChanged(nameof(SubscriptionButtonBackgroundColor));
            }
        }

        public bool IsProSubscriptionActive
        {
            get => _isProSubscriptionActive;
            set
            {
                _isProSubscriptionActive = value;

                OnPropertyChanged(nameof(IsProSubscriptionActive));
                OnPropertyChanged(nameof(ProStatusString));
            }
        }

        public ICommand GoToMyIdeasCommand => new AsyncRelayCommand(GoToMyIdeas);

        public ICommand CreateNewIdeaCommand => new AsyncRelayCommand(CreateNewIdea);

        public ICommand GoToSettingsCommand => new AsyncRelayCommand(GoToSettings);

        public ICommand GoToSubscriptionCommand => new AsyncRelayCommand(GoToSubscription);

        public Color MyIdeasButtonBackgroundColor => IsMyIdeasSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        public Color CreateIdeaButtonBackgroundColor => IsNewIdeaSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        public Color SubscriptionButtonBackgroundColor => IsSubscriptionSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        public string ProStatusString => IsProSubscriptionActive ? "Pro Version" : "Free Version";

        public void OnAppearing()
        {
            IsMyIdeasSelected = false;
            IsNewIdeaSelected = false;
            IsSubscriptionSelected = false;
            IsProSubscriptionActive = GetSubscriptionStatus();
        }

        private async Task GoToMyIdeas()
        {
            IsMyIdeasSelected = true;

            await Shell.Current.GoToAsync(nameof(IdeasListPage));
        }

        private async Task CreateNewIdea()
        {
            IsNewIdeaSelected = true;

            await Shell.Current.GoToAsync(nameof(IdeaPage));
        }

        private async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async Task GoToSubscription()
        {
            IsSubscriptionSelected = true;

            await Shell.Current.GoToAsync(nameof(SubscriptionPage));
        }

        private bool GetSubscriptionStatus()
        {
            var subExpirationDate = Preferences.Get("SubExpirationDate", DateTime.MinValue);
            var hasPurchasedSub = Preferences.Get("HasPurchasedSub", true);
            return hasPurchasedSub && subExpirationDate > DateTime.UtcNow;
        }
    }
}

