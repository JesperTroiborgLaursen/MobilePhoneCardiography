using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DTOs;
using MvvmHelpers;
using Xamarin.Forms;

namespace DataAccessLayer.Services.Interface
{
    public interface IRecordinsViewModel
    {
        

        public Patient SelectedPatient { get; set; }

        public ObservableCollection<Measurement> Measurements { get; }
        public Command LoadMeasurementsCommand { get; }
        public Command AddMeasurementCommand { get; }
        public Command<Measurement> MeasurementTapped { get; }
        public ICommand NewRecordingCommand { get; }

    }
}