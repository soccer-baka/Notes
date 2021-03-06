﻿using System;
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
                using (var stringReader = new System.IO.StringReader(text))
                {
                    timestamp = DateTime.Parse(stringReader.ReadLine());
                    content = stringReader.ReadToEnd();
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

        public async Task WriteAll()
        {
            await Task.WhenAll(Notes.Where(note => note.IsModified).Select(async note =>
            {
                await note.Write();
            }));
        }

        public async Task Write(NoteData note)
        {
            await note.Write();
            Sort();
        }

        public async Task Delete(NoteData note)
        {
            Notes.Remove(note);

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
