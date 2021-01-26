using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectHB
{
    public partial class App : Application
    {
        static Databases database;

        public static Databases Database
        {
            get
            {
                if (database == null)
                {
                    database = new Databases(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydb.db"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
