using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Notes
{
    public class NoteData : INotifyPropertyChanged
    {
        private const int previewLength = 24;

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
