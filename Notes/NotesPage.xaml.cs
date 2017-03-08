using Xamarin.Forms;
using System;
using PCLStorage;

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
            GetFiles();

            NoteData[] temp = { new NoteData("test 1"), new NoteData("note 2") };
            listView.ItemsSource = temp;
        }

        private async void GetFiles()
        {
			var folder = FileSystem.Current.LocalStorage;
			var files = await folder.GetFilesAsync().ConfigureAwait(false);
            foreach (var file in files)
            {
                System.Diagnostics.Debug.WriteLine(file);
            }
		}

        private async void ListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.SelectedItem} was selected.");
            await Navigation.PushAsync(new Note());
            //listView.SelectedItem = null;
        }

        private async void OnNew(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new Note());
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", menuItem.CommandParameter + " delete context action", "OK");
        }
    }
}
