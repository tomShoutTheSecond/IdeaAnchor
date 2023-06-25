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

        public ICommand GoToMyIdeasCommand => new AsyncRelayCommand(GoToMyIdeas);

        public ICommand CreateNewIdeaCommand => new AsyncRelayCommand(CreateNewIdea);

        public Color MyIdeasButtonBackgroundColor => IsMyIdeasSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        public Color CreateIdeaButtonBackgroundColor => IsNewIdeaSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        public void OnAppearing()
        {
            IsMyIdeasSelected = false;
            IsNewIdeaSelected = false;
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
    }
}

