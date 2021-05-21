﻿using System;
using System.ComponentModel;
using DataAccessLayer.Services.Interface;
using MobilePhoneCardiography.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePhoneCardiography.Views
{
    public partial class RecordingsView : ContentPage
    {
        RecordingsViewModel _viewModel;
        public RecordingsView()
        {
            InitializeComponent();
            BindingContext = App.IoCContainer.GetInstance<IRecordinsViewModel>();
            _viewModel = new RecordingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}