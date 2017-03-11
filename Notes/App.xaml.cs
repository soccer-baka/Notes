using Xamarin.Forms;

namespace Notes
{
    public partial class App : Application
    {
        static public NotesModel notesModel = new NotesModel();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NoteListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
