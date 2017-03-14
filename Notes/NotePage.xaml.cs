using System;

using Xamarin.Forms;

namespace Notes
{
    public partial class NotePage : ContentPage
    {
        private string originalContent;
        private NoteData currentNote;

        public NotePage(NoteData note=null)
        {
            InitializeComponent();
            currentNote = note;
            editorNote.Text = note == null ? "" : note.Content;
            originalContent = editorNote.Text;
        }

        protected override async void OnDisappearing()
        {
            if (editorNote.Text != originalContent)
            {
                if (currentNote == null)
                {
                    currentNote = new NoteData(App.notesViewModel.GenerateFileName(), editorNote.Text);
                    App.notesViewModel.Notes.Add(currentNote);
                }
                else
                {
                    currentNote.Content = editorNote.Text;
                }
                await App.notesViewModel.Write(currentNote);
            }
        }
    }
}
