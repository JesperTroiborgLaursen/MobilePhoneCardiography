using System;
using System.ComponentModel;
using Microcharts;
using Microcharts.Forms;
using MobilePhoneCardiography.ViewModels;
using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EventArgss;

namespace MobilePhoneCardiography.Views
{
    public partial class MeasureView : ContentPage
    {
        MeasureViewModel _viewModel;

        TimeSpan _animationTimeSpan;

        public MeasureView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MeasureViewModel();

            _viewModel.graphReadyEvent += HandleGraphReadyEvent;
            //Added temp

            _animationTimeSpan = new TimeSpan(1);

        }


        private void HandleGraphReadyEvent(object s, GraphReadyEventArgs e)
        {
          
            chartView.Chart = new LineChart { 
                Entries = e.ChartValues, 
                IsAnimated = false, 
                LineSize = (float)1, 
                PointMode = 0, 
                EnableYFadeOutGradient = false, 
                LineMode = (LineMode)2, 
                AnimationDuration = _animationTimeSpan }; //IsAnimated = false, AnimationProgress = (float)0, LineSize = (float)0.1, PointMode = 0 };
            chartView.CancelAnimations();
        }

    }
}