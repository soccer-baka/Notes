using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Notes
{
    public partial class NoteListPage : ContentPage
    {
        public NoteListPage()
        {
            InitializeComponent();
			listView.BindingContext = App.notesModel.notes;
        }

		private async void ListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"{e.SelectedItem} was selected.");
            await Navigation.PushAsync(new NotePage((NoteData)e.SelectedItem));
			//listView.SelectedItem = null;
		}

		private async void OnNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NotePage());
		}

		private void OnDelete(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			DisplayAlert("Delete Context Action", menuItem.CommandParameter + " delete context action", "OK");
		}

        protected override void OnAppearing()
        {
        }
	}
}
