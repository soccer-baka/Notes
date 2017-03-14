using Xamarin.Forms;

namespace Notes
{
    public partial class App : Application
    {
        static public NotesViewModel notesViewModel = new NotesViewModel();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NoteListPage());
        }

        protected override async void OnStart()
        {
            await notesViewModel.ReadAll();
        }

        //protected override async void OnSleep()
        //{
            //await notesModel.WriteAll();
        //}

        //protected override void OnResume()
        //{
            // Handle when your app resumes
        //}
    }
}
