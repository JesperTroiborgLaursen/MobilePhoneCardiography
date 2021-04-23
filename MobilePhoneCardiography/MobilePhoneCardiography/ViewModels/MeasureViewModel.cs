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

namespace MobilePhoneCardiography.ViewModels
{
    public class MeasureViewModel : BaseViewModel

    {


        public DTOs.Measurement MeasureDTO { get; set; }

        private IRecorderController _recorderController;
        private List<object> _placement;
        public ICommand OpenWebCommand { get; }

        public ICommand RecordAudioCommand { get; }
        public ICommand PlacementInfoCommand { get; }
        public List<object> Placement
        {
            get => _placement;
            set => SetProperty(ref _placement, value);
        }

        public MeasureViewModel()
        {
            Title = "Measure";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            RecordAudioCommand = new Command(StartRecordTask);
            PlacementInfoCommand = new Command(OnPlacementInfoClicked);
            _recorderController = new RecorderController(HandleAnalyzeFinishedEvent);

            Placement = new List<object>();
            Placement.Add(PlacementOfDeviceEnum.CorDexter.ToString());
            Placement.Add(PlacementOfDeviceEnum.CorInfra.ToString());
            Placement.Add(PlacementOfDeviceEnum.CorSinister.ToString());
        }




        private void StartRecordTask()
        {
            _recorderController.RecordAudio();
        }

        private async void OnPlacementInfoClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(MeasureView)}");
        }

        private void HandleAnalyzeFinishedEvent(object sender, AnalyzeFinishedEventArgs e)
        {

            MeasureDTO = e.DTO;
            //Todo Denne linje skal væk når vi har introduceret RecordingsViewet
            //da den på nuværende tidspunkt blot afspiller lyden med det samme
            _recorderController.PlayRecording(MeasureDTO);
        }

    }
}