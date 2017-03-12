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

        private async void ListItemTapped(object sender, ItemTappedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.Item} was selected.");
            await Navigation.PushAsync(new NotePage((NoteData)e.Item));
            //listView.SelectedItem = null;
        }

        private async void OnNew(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotePage());
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            NoteData note = (NoteData)menuItem.CommandParameter;
            App.notesModel.notes.Remove(note);
			await App.notesModel.Delete(note);
            //DisplayAlert("Delete Context Action", menuItem.CommandParameter + " delete context action", "OK");
        }

        protected override void OnAppearing()
        {
        }
    }
}
