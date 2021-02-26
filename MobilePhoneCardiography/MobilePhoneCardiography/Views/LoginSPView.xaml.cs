using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePhoneCardiography.Views
{
    public partial class LoginSPView : ContentPage
    {
        public Item User { get; set; }

        public LoginSPView()
        {
            InitializeComponent();
            BindingContext = new LoginSPViewModel();
        }
    }
}