using Xamarin.Forms;
using System;
//using PCLStorage;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        class NoteData
        {
            public string Filename { get; set; }
            public NoteData(string name)
            {
                Filename = name;
            }
        }

        public NotesPage()
        {
            InitializeComponent();

            //var folder = FileSystem.Current.LocalStorage;
            //var files = folder.GetFilesAsync();

            NoteData[] temp = { new NoteData("test 1"), new NoteData("note 2") };
            listView.ItemsSource = temp;
        }

        private async void ListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.SelectedItem} was selected.");
            await Navigation.PushAsync(new Note());
            //listView.SelectedItem = null;
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", menuItem.CommandParameter + " delete context action", "OK");
        }
    }
}
