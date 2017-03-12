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

        protected override async void OnStart()
        {
            await notesModel.ReadAll();
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
