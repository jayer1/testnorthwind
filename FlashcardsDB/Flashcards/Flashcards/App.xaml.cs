using System;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Flashcards
{
    public partial class App : Application
    {
        static FlashcardsDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        

        public static FlashcardsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FlashcardsDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FlashcardsSQLite.db3"));
                }
                return database;
            }
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
