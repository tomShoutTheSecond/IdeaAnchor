using System;
using CommunityToolkit.Mvvm.Input;
using IdeaAnchor.Services;

namespace IdeaAnchor.MenuProviders
{
    public class SettingsMenuProvider
    {
        private ImportExportService _importExportService;

        public SettingsMenuProvider(ImportExportService importExportService)
        {
            _importExportService = importExportService;
        }

        public List<AppMenuItem> GetMenu(Action<int, bool> selectedAction)
        {
            return new List<AppMenuItem>
            {
                new AppMenuItem
                {
                    Name = "Import Ideas",
                    Icon = "\uf56f",
                    Command = new AsyncRelayCommand(() => SelectedRunCommand(0, selectedAction, _importExportService.ImportIdeas))
                },
                new AppMenuItem
                {
                    Name = "Export Ideas",
                    Icon = "\uf56e",
                    Command = new AsyncRelayCommand(() => SelectedRunCommand(1, selectedAction, _importExportService.ExportIdeas))
                }
            };
        }

        /// <summary>
        /// Notifies the UI layer when items are selected and deselected
        /// </summary>
        private async Task SelectedRunCommand(int itemIndex, Action<int, bool> selectedAction, Func<Task> commandAction)
        {
            selectedAction?.Invoke(itemIndex, true);

            await commandAction();

            selectedAction?.Invoke(itemIndex, false);
        }
    }
}

