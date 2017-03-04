using Xamarin.Forms;

namespace Notes
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
            string[] temp = { "note 1", "note 2" };
            listView.ItemsSource = temp;
        }

        private async void ListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.SelectedItem} was selected.");
            await Navigation.PushAsync(new Note());
            //listView.SelectedItem = null;
        }
    }
}
