using System;

using Xamarin.Forms;

namespace Notes
{
    public partial class NotePage : ContentPage
    {
        private NoteData currentNote;

        public NotePage(NoteData note=null)
        {
            InitializeComponent();
            currentNote = note;
            if (currentNote == null)
            {
                currentNote = new NoteData(App.notesViewModel.GenerateFileName(), "");
                App.notesViewModel.Notes.Add(currentNote);
            }
            BindingContext = currentNote;
        }

        protected override async void OnDisappearing()
        {
			if (currentNote.IsEmpty)
			{
				await App.notesViewModel.Delete(currentNote);
			}
			else if (currentNote.IsModified)
            {
                await App.notesViewModel.Write(currentNote);
            }
        }
    }
}
