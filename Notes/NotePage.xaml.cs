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
                    currentNote = new NoteData(App.notesModel.GenerateFileName(), editorNote.Text);
                    App.notesModel.notes.Add(currentNote);
                }
                else
                {
                    currentNote.Content = editorNote.Text;
                }
                await App.notesModel.Write(currentNote);
            }
        }
    }
}
