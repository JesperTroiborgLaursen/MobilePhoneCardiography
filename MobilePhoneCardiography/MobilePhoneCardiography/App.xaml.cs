using MobilePhoneCardiography.Services;
using MobilePhoneCardiography.Views;
using System;
using BusinessLogic;
using DataAccessLayer.Services;
using EventArgss;
using Xamarin.Forms;

namespace MobilePhoneCardiography
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStoreUser>();
            DependencyService.Register<MockDataStorePatient>();
            DependencyService.Register<MockDataStoreMeasurement>();
            DependencyService.Register<RecorderController>();
            MainPage = new AppShell();


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
