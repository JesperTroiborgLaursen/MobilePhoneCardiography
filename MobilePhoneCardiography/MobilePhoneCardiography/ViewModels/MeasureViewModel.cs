using System;
using System.Linq;
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
using System.Threading;
using System.Collections.Generic;

namespace MobilePhoneCardiography.ViewModels
{
    public class MeasureViewModel : BaseViewModel

    {
        #region Attributter og dependencies

        public string Name { get; set; }
        public string SocSec { get; set; }
        public ObservableCollection<Measurement> Measurements { get; }
        public event EventHandler<GraphReadyEventArgs> graphReadyEvent;

        private Measurement _selectedMeasurement;

        public DTOs.Measurement MeasureDTO { get; set; }

        private IRecorderController _recorderController;
        private List<object> _placement;
        public ICommand OpenWebCommand { get; }

        public ICommand RecordAudioCommand { get; }
        public ICommand PlacementInfoCommand { get; }

        public bool StartVisible { get; set; }
        public bool StopVisible { get; set; }

        public List<object> Placement
        {
            get => _placement;
            set => SetProperty(ref _placement, value);
        }

        public ChartEntry[] ChartValuesMvm { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Class to show the graph view on the application
        /// </summary>
        public MeasureViewModel()
        {
            Title = "Measure";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            RecordAudioCommand = new Command(StartRecordTask);
            PlacementInfoCommand = new Command(OnPlacementInfoClicked);
            _recorderController = new RecorderController(HandleAnalyzeFinishedEvent);

            StartVisible = true;
            StopVisible = false;

            Placement = new List<object>();
            Placement.Add(PlacementOfDeviceEnum.URSB.ToString());
            Placement.Add(PlacementOfDeviceEnum.ULSB.ToString());
            Placement.Add(PlacementOfDeviceEnum.LLSB.ToString());
            Placement.Add(PlacementOfDeviceEnum.Apex.ToString());

        }

        #endregion
        #region Methods


        /// <summary>
        /// Kalder RecordAudio() i BusinessLogic layer
        /// </summary>
        private void StartRecordTask()
        {
            StartVisible = false;
            StopVisible = true;
            _recorderController.RecordAudio();
        }

        private async void OnPlacementInfoClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(PlacementInfoView)}");
        }


        /// <summary>
        /// Event der kaldes når optagelsen er færdig med at analysere for hjerte mislyde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAnalyzeFinishedEvent(object sender, AnalyzeFinishedEventArgs e)
        {

            MeasureDTO = e.DTO;
            //Todo Denne linje skal væk når vi har introduceret RecordingsViewet
            //da den på nuværende tidspunkt blot afspiller lyden med det samme
            StartVisible = true;
            StopVisible = false;
            _recorderController.PlayRecording(MeasureDTO);
            ChartValuesMvm = _recorderController.ChartValues;

            if (ChartValuesMvm != null)
            {
                OnGraphReady(new GraphReadyEventArgs { ChartValues = ChartValuesMvm });
            }
        }

        private void OnGraphReady(GraphReadyEventArgs e)
        {
            graphReadyEvent?.Invoke(this, e);
        }
        #endregion
    }
}