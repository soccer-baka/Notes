using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PCLStorage;

namespace Notes
{
    public class NotesModel
    {
        public ObservableCollection<NoteData> notes { get; private set; }

        public NotesModel()
        {
            notes = new ObservableCollection<NoteData>();
            //ReadAll();
            MakeTestData();
        }

        public async void ReadAll()
        {
            var folder = FileSystem.Current.LocalStorage;
            var files = await folder.GetFilesAsync();
            NoteData[] noteArray = await Task.WhenAll(files.Select(async file =>
            {
                var content = await file.ReadAllTextAsync();
                return new NoteData(file.Name, content);
            }));
            foreach (var note in noteArray)
            {
                notes.Add(note);
            }
        }

        public async void Write(NoteData note)
        {
			var folder = FileSystem.Current.LocalStorage;
			var file = await folder.CreateFileAsync(note.Filename, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(note.Content);
        }

        public async void Delete(NoteData note)
        {
			var folder = FileSystem.Current.LocalStorage;
			var file = await folder.GetFileAsync(note.Filename);
            await file.DeleteAsync();
        }

        private void MakeTestData()
        {
            for (int i = 0; i < 4; i++)
            {
                notes.Add(new NoteData($"note {i}", $"comment {i}", DateTime.Now));
            }
        }
    }

    public class NoteData : INotifyPropertyChanged
    {

		public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get; private set; }
        public DateTime Timestamp { get; private set; }
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                Timestamp = DateTime.Now;
				OnPropertyChanged("Content");
                OnPropertyChanged("Preview");
				OnPropertyChanged("Timestamp");
            }
        }
        public string Preview
        {
            get
            {
                return content.Length < 10 ? content : content.Substring(0, 10);
            }
        }
        public NoteData(string name, string _content) : this(name, _content, DateTime.Now) { }
        public NoteData(string name, string _content, DateTime time)
        {
            Filename = name;
            content = _content;
            Timestamp = time;
        }

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = this.PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}
