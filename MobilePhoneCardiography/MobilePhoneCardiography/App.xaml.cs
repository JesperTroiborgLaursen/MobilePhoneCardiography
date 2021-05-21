using MobilePhoneCardiography.Services;
using MobilePhoneCardiography.Views;
using System;
using BusinessLogic;
using DataAccessLayer;
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
        private static Container ioCContainer = new SimpleInjector.Container();
        public static Container IoCContainer
        {
            get => ioCContainer;
            set => ioCContainer = value;
        }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStoreUser>();
            DependencyService.Register<MockDataStorePatient>();
            DependencyService.Register<MockDataStoreMeasurement>();
            IoCContainer.Register<IRecorder, Recorder>();
            IoCContainer.Register<ISoundModifyLogic, SoundModifyLogic>();
            IoCContainer.Register<ISaveData, FakeStorage>(); ;
            IoCContainer.Register<IRecorderController, RecorderController>(Lifestyle.Singleton);
            IoCContainer.Register<IAnalyzeLogic, AnalyzeLogic>(Lifestyle.Singleton);
            IoCContainer.Register<BaseViewModel, MeasureViewModel>(Lifestyle.Singleton);
            ioCContainer.Verify();
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
