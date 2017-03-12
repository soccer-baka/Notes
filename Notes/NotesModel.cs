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
        }

        public async Task ReadAll()
        {
            var folder = FileSystem.Current.LocalStorage;
            var files = await folder.GetFilesAsync();
            NoteData[] noteArray = await Task.WhenAll(files.Select(async file =>
            {
                var text = await file.ReadAllTextAsync();
                var timestamp = DateTime.Now;
                var content = "";
                using (var rs = new System.IO.StringReader(text))
                {
                    var line = 0;
                    while (rs.Peek() > -1)
                    {
                        if (line == 0)
                        {
                            timestamp = DateTime.Parse(rs.ReadLine());
                        }
                        else
                        {
                            content += rs.ReadLine() + Environment.NewLine;
                        }
                        line++;
                    }
                }
                return new NoteData(file.Name, content, timestamp);
            }));
            foreach (var note in noteArray)
            {
                notes.Add(note);
            }
        }

        //public async Task WriteAll()
        //{
        //}

        public async Task Write(NoteData note)
        {
            var folder = FileSystem.Current.LocalStorage;
            var file = await folder.CreateFileAsync(note.Filename, CreationCollisionOption.ReplaceExisting);
            var text = note.Timestamp.ToString() + Environment.NewLine + note.Content;
            await file.WriteAllTextAsync(text);
        }

        public async Task Delete(NoteData note)
        {
            var folder = FileSystem.Current.LocalStorage;
            var file = await folder.GetFileAsync(note.Filename);
            await file.DeleteAsync();
        }

        public string GenerateFileName()
        {
            var filenameList = from n in notes
                               orderby n.Filename
                               select n.Filename;
            string name = "note000.txt";
            int number = 0;
            foreach (var filename in filenameList)
            {
                if (name != filename)
                {
                    return name;
                }
                else
                {
                    number++;
                    name = String.Format("note{0:D3}.txt", number);
                }
            }
            return name;
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
