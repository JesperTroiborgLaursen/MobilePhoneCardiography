﻿using MobilePhoneCardiography.Views;
using System;
using System.Collections.Generic;
using System.Text;
using MobilePhoneCardiography.Models;
using MobilePhoneCardiography.Services.DataStore;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginSPCommand { get; }
        public Command LoginPBCommand { get; }

     

        public LoginViewModel()
        {
            LoginSPCommand = new Command(OnLoginSPClicked);
            LoginPBCommand = new Command(OnLoginPBClicked);
          }

        private async void OnLoginPBClicked(object obj)
        {


            //// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(MeasureView)}");




        }

        private async void OnLoginSPClicked(object obj)
        {

          
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(LoginSPView)}");
        }


    }
}
