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

        public List<AppMenuItem> GetMenu()
        {
            return new List<AppMenuItem>
            {
                new AppMenuItem
                {
                    Name = "Import Ideas",
                    Icon = "\uf56f",
                    Command = new AsyncRelayCommand(_importExportService.ImportIdeas)
                },
                new AppMenuItem
                {
                    Name = "Export Ideas",
                    Icon = "\uf56e",
                    Command = new AsyncRelayCommand(_importExportService.ExportIdeas)
                }
            };
        }
    }
}

