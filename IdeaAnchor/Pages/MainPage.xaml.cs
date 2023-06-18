﻿using IdeaAnchor.Pages;

namespace IdeaAnchor;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private async void CreateNewIdea(object sender, EventArgs e)
    {
		//navigate to idea page
		await Shell.Current.GoToAsync(nameof(IdeaPage));
    }
}


