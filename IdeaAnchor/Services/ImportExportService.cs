using System;
using ServiceStack;
using IdeaAnchor.Models;
using IdeaAnchor.Database;
using CommunityToolkit.Maui.Storage;
using System.Text;

namespace IdeaAnchor.Services
{
	public class ImportExportService
	{
        private readonly IdeaDatabase _db;

        public ImportExportService(IdeaDatabase db)
        {
            _db = db;
        }

		public async Task ImportIdeas()
		{
            try
            {
                await Shell.Current.CurrentPage.DisplayAlert("Import Ideas", "Select a .csv file exported from IdeaAnchor", "OK");

                var fileResult = await PickFile(GetFilePickerOptions());
                if (fileResult == null)
                    return; //user cancelled

                var content = await File.ReadAllTextAsync(fileResult.FullPath);

                var importedIdeas = content.FromCsv<List<Idea>>();

                var newIdeasCount = 0;
                foreach(var idea in importedIdeas)
                {
                    idea.Id = null; //make sure the DB inserts a new idea

                    newIdeasCount += await _db.SaveIdeaAsync(idea);
                }

                await Shell.Current.CurrentPage.DisplayAlert("Success", $"{newIdeasCount} ideas imported", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Import Error", e.Message, "OK");
            }
        }

		public async Task ExportIdeas()
		{
            try
            {
                var ideas = await _db.GetIdeasAsync();

                //remove the ID field as it's not required
                //we will create new IDs for the imported files when importing
                var csvIdeas = ideas.Select(i => ToCsvIdea(i));

                var csv = csvIdeas.ToCsv();

                await SaveFile(csv);

                await Shell.Current.CurrentPage.DisplayAlert("Success", $"{ideas.Count} ideas exported", "OK");
            }
            catch(TaskCanceledException)
            {
                //user cancelled
            }
            catch(Exception e)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Export Error", e.Message, "OK");
            }
        }

        private async Task<FileResult> PickFile(PickOptions options)
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("csv", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

            return result;
        }

        private PickOptions GetFilePickerOptions()
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.comma-separated-values-text" } }, // UTType values
                    { DevicePlatform.Android, new[] { "text/comma-separated-values" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".csv" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "public.comma-separated-values-text" } }, // UTType values
                });

            var options = new PickOptions()
            {
                PickerTitle = "Select a CSV file to import",
                FileTypes = customFileType,
            };

            return options;
        }

        private async Task SaveFile(string csv)
        {
            using var stream = new MemoryStream(Encoding.Default.GetBytes(csv));
            var result = await FileSaver.SaveAsync("IdeaAnchor_Export.csv", stream, CancellationToken.None);
            result.EnsureSuccess();
        }

        private CsvIdea ToCsvIdea(Idea idea)
        {
            return new CsvIdea
            {
                Title = idea.Title,
                Content = idea.Content,
                CreatedTime = idea.CreatedTime,
                LastUpdatedTime = idea.LastUpdatedTime
            };
        }
    }
}

