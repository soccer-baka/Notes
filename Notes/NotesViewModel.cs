using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using PCLStorage;

namespace Notes
{
    public class NotesViewModel
    {
        public ObservableCollection<NoteData> Notes { get; } = new ObservableCollection<NoteData>();

        public NotesViewModel()
        {
        }

        private void Sort()
        {
			List<NoteData> tmpList = new List<NoteData>(Notes);
			var query = tmpList.OrderByDescending(n => n.Timestamp);
			Notes.Clear();
			foreach (var note in query)
			{
				Notes.Add(note);
			}
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
                    timestamp = DateTime.Parse(rs.ReadLine());
                    content = rs.ReadToEnd();
                }
                return new NoteData(file.Name, content, timestamp);
            }));
            Notes.Clear();
			var query = noteArray.OrderByDescending(n => n.Timestamp);
            foreach (var note in query)
            {
                Notes.Add(note);
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
            Sort();
        }

        public async Task Delete(NoteData note)
        {
            var folder = FileSystem.Current.LocalStorage;
            var file = await folder.GetFileAsync(note.Filename);
            await file.DeleteAsync();
        }

        public string GenerateFileName()
        {
            var filenameList = from n in Notes
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

}
