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
using System.Linq;

namespace MobilePhoneCardiography.Views
{
    public partial class MeasureView : ContentPage
    {
        MeasureViewModel _viewModel;

        TimeSpan _animationTimeSpan;

        /// <summary>
        /// Design of graph
        /// </summary>
        public MeasureView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MeasureViewModel();

            _viewModel.graphReadyEvent += HandleGraphReadyEvent;
            //Added temp

            _animationTimeSpan = new TimeSpan(1);

        }

        /// <summary>
        /// Event is triggered when the ChartEntry points are sat to a value. 
        /// The mehtod plots the heart sound graph
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleGraphReadyEvent(object s, GraphReadyEventArgs e)
        {
            chartView.Chart = new LineChart {
                Entries = e.ChartValues,
                IsAnimated = false, 
                LineSize = (float)0.5, 
                PointMode = 0, 
                EnableYFadeOutGradient = false, 
                LineMode = (LineMode)2, 
                AnimationDuration = _animationTimeSpan,
                AnimationProgress = (float)0};
            chartView.CancelAnimations();

        }

    }
}