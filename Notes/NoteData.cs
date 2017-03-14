using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Notes
{
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
				string preview;
				using (var rs = new System.IO.StringReader(content))
				{
					preview = rs.ReadLine();
				}
				return preview.Length < 24 ? preview : preview.Substring(0, 24) + "...";
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
