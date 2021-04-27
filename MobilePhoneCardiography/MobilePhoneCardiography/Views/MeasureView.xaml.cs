using System;
using System.ComponentModel;
using Microcharts;
using Microcharts.Forms;
using MobilePhoneCardiography.ViewModels;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EventArgss;

namespace MobilePhoneCardiography.Views
{
    public partial class MeasureView : ContentPage 
    {
        MeasureViewModel _viewModel;

        TimeSpan timeSpan;
        

        public MeasureView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MeasureViewModel();

            _viewModel.graphReadyEvent += HandleGraphReadyEvent;

        }


        private void HandleGraphReadyEvent(object s, GraphReadyEventArgs e)
        {
            if (_viewModel.oldEntries == null)
            {
                _viewModel.oldEntries = e.ChartValues;
                chartView.Chart = new LineChart { Entries = _viewModel.oldEntries, IsAnimated = false, LineSize = (float)1, PointMode = 0, EnableYFadeOutGradient = false, LineMode = (LineMode)2 };
            }

            else if (_viewModel.oldEntries != null)
            {
                _viewModel.newEntries = e.ChartValues;
                _viewModel.combinedEntries = new ChartEntry[_viewModel.oldEntries.Length + _viewModel.newEntries.Length];
                for (int i = 0; i < _viewModel.combinedEntries.Length; ++i)
                {
                    _viewModel.combinedEntries[i] = i < _viewModel.oldEntries.Length ? _viewModel.oldEntries[i] : _viewModel.newEntries[i - _viewModel.oldEntries.Length];
                }
                chartView.Chart = new LineChart { Entries = _viewModel.combinedEntries, IsAnimated = false, LineSize = (float)1, PointMode = 0, EnableYFadeOutGradient = false, LineMode = (LineMode)2 };
                _viewModel.oldEntries = _viewModel.combinedEntries;
                chartView.CancelAnimations();
            }

            else { /*Do nothing*/}
        }
    }
}