﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppMovil
{
    public partial class App : Application
    {
        public static string DatabasePath = string.Empty;

        public App(string databasePath)
        {
            InitializeComponent();
            DatabasePath = databasePath;
            MainPage = new NavigationPage(new PageInicio());
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageInicio());
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
