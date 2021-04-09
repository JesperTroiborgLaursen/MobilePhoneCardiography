using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic;
using DTOs;
using EventArgss;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microcharts.Forms;
using Microcharts;
using SkiaSharp;

namespace MobilePhoneCardiography.ViewModels
{
    public class MeasureViewModel : BaseViewModel

    {
        #region Attributter og dependencies

        private Measurement _selectedMeasurement;

        public ObservableCollection<Measurement> Measurements { get; }
        public Command LoadMeasurementsCommand { get; }
        public Command AddMeasurementCommand { get; }
        public Command<Measurement> MeasurementTapped { get; }

        private IRecorderController _recorderController;
        public ICommand OpenWebCommand { get; }

        public ICommand NewRecordingCommand { get; }
        

        public ICommand RecordAudioCommand { get; }

        public ChartEntry[] ChartValuesMvm { get; set; }
        #endregion
        #region Constructor
        public MeasureViewModel()
        {
            Title = "Measure";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            NewRecordingCommand = new Command(OnNewRecordingClicked);

            Measurements = new ObservableCollection<Measurement>();
            LoadMeasurementsCommand = new Command(async () => await ExecuteLoadMeasurementsCommand());

            MeasurementTapped = new Command<Measurement>(OnItemSelected);

            AddMeasurementCommand = new Command(OnAddMeasurement);

            RecordAudioCommand = new Command(StartRecordTask);
            _recorderController = new RecorderController(HandleAnalyzeFinishedEvent);
        }

        #endregion
        #region Methods
        private async void OnNewRecordingClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(MeasureView)}");
        }

        async Task ExecuteLoadMeasurementsCommand()
        {
            IsBusy = true;

            try
            {
                Measurements.Clear();
                var measurements = await DataStoreUserMeasurement.GetItemsAsync(true);
                foreach (var measurement in measurements)
                {
                    Measurements.Add(measurement);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMeasurement = null;
        }


     
        public Measurement SelectedMeasurement
        {
            get => _selectedMeasurement;
            set
            {
                SetProperty(ref _selectedMeasurement, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddMeasurement(object obj)
        {
            await Shell.Current.GoToAsync(nameof(LoginSPView));
        }

        async void OnItemSelected(Measurement measurement)
        {
            if (measurement == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={measurement.Id}");
        }

        private void StartRecordTask()
        {
            _recorderController.RecordAudio();
            ChartValuesMvm = _recorderController.ChartValues;
            //_measureView.SetChartValues();
        }

        public DTOs.Measurement MeasureDTO { get; set; }

        private void HandleAnalyzeFinishedEvent(object sender, AnalyzeFinishedEventArgs e)
        {

            MeasureDTO = e.DTO;
            //Todo Denne linje skal væk når vi har introduceret RecordingsViewet
            //da den på nuværende tidspunkt blot afspiller lyden med det samme
            _recorderController.PlayRecording(MeasureDTO);
           
        }
        #endregion
    }
}