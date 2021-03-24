﻿using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Azure.Documents;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePhoneCardiography.Views
{
    public partial class FindPatientView : ContentPage
    {
        public User User { get; set; }

        public FindPatientView()
        {
            InitializeComponent();
            BindingContext = new FindPatientViewModel();
        }
    }
}