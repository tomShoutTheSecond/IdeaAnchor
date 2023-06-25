﻿using IdeaAnchor.ViewModels;

namespace IdeaAnchor.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
