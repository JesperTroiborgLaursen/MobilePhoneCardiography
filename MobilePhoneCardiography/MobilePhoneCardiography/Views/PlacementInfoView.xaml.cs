using System;
using System.ComponentModel;
using MobilePhoneCardiography.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePhoneCardiography.Views
{
    public partial class PlacementInfoView : ContentPage
    {
        PlacementInfoViewModel _viewModel;
        public PlacementInfoView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PlacementInfoViewModel();
        }


    }
}