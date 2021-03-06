﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FinalProject
{
    public partial class App : Application
    {
        static FinalProjectDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static FinalProjectDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FinalProjectDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FinalProjectDatabaseSQLite.db3"));
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
