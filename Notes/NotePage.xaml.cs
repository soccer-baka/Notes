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

        protected override void OnDisappearing()
        {
            if (currentNote == null)
            {
                App.notesModel.notes.Add(new NoteData("new", editorNote.Text));
            }
            else if (editorNote.Text != originalContent)
            {
                currentNote.Content = editorNote.Text;
            }
        }
    }
}
