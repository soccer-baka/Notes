using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PCLStorage;

namespace Notes
{
    public class NoteData : INotifyPropertyChanged
    {
        private const int previewLength = 24;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get; private set; }
        public DateTime Timestamp { get; private set; }
        private string originalContent;
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
                string preview;
                using (var stringReader = new System.IO.StringReader(content))
                {
                    preview = stringReader.ReadLine();
                    preview = preview ?? "";
                }
                return preview.Length < previewLength ? preview : preview.Substring(0, previewLength) + "...";
            }
        }
        public bool IsEmpty
        {
            get
            {
                return content == "";
            }
        }
        public bool IsModified
        {
            get
            {
                return content != originalContent;
            }
        }

        public NoteData(string name, string _content) : this(name, _content, DateTime.Now) { }
        public NoteData(string name, string _content, DateTime time)
        {
            Filename = name;
            content = _content;
            originalContent = content;
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

        public async Task Write()
        {
            Timestamp = DateTime.Now;
			originalContent = content;

			var folder = FileSystem.Current.LocalStorage;
			var file = await folder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
			var text = Timestamp.ToString() + Environment.NewLine + Content;
			await file.WriteAllTextAsync(text);
        }
    }
}
