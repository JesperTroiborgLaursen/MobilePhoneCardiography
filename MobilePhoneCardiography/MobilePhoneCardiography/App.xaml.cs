using MobilePhoneCardiography.Services;
using MobilePhoneCardiography.Views;
using System;
using DataAccessLayer.Services;
using DataAccessLayer.Services.Interface;
using EventArgss;
using MobilePhoneCardiography.ViewModels;
using SimpleInjector;
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
            App.IoCContainer.Register<IRecordinsViewModel, RecordingsViewModel>(Lifestyle.Singleton);
            IoCContainer.Verify();
            MainPage = new AppShell();


        }

        private static Container ioCContainer = new SimpleInjector.Container();
        public static Container IoCContainer
        {
            get => ioCContainer;
            set => ioCContainer = value;
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
