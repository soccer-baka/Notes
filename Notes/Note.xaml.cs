using System;
using System.Collections.Generic;
using PCLStorage;

using Xamarin.Forms;

namespace Notes
{
    public partial class Note : ContentPage
    {
        public Note() : this(null) { }

        public Note(string filename)
        {
            InitializeComponent();
            if (filename == null)
            {
            }
            else
            {
            }
        }

        private async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
			var folder = FileSystem.Current.LocalStorage;
			var file = await folder.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(editorNote.Text);
            await Navigation.PopAsync();
        }
    }
}
