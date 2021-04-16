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
            //Added temp

            timeSpan = new TimeSpan(3);


        }
     

        private void HandleGraphReadyEvent(object s, GraphReadyEventArgs e)
        {
            var entries = new[]
          {
                 new Microcharts.ChartEntry(212)
                 {
                     Label = "UWP",
                     ValueLabel = "112",
                     Color = SKColor.Parse("#2c3e50")
                 },
                 new ChartEntry(248)
                 {
                     Label = "Android",
                     ValueLabel = "648",
                     Color = SKColor.Parse("#77d065")
                 },
                 new ChartEntry(128)
                 {
                     Label = "iOS",
                     ValueLabel = "428",
                     Color = SKColor.Parse("#b455b6")
                 },
             };

            chartView.Chart = new LineChart { Entries = e.ChartValues, IsAnimated = false, LineSize = (float)1, PointMode = 0, EnableYFadeOutGradient = false, LineMode = (LineMode)2 }; //AnimationDuration = timeSpan, IsAnimated = false, AnimationProgress = (float)0, LineSize = (float)0.1, PointMode = 0 };
            chartView.CancelAnimations();

            //chartView.Effects.Clear();
          
          

            
        }

    }
}