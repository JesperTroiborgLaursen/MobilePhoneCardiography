﻿
using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using DataAccessLayer;
using DTOs;
using MobilePhoneCardiography.Services.DataStore;
using MobilePhoneCardiography.Views;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{
    public class LoginSPViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private ControllerDatabase controllerDatabase;

        //TODO SKAL FJERNES IGEN; KUN ITL AT TESTE DATABASE
       
        public LoginSPViewModel()
        {
            controllerDatabase = new ControllerDatabase(new CosmosDBService(EnumDatabase.Professionel, DateTime.Now));


            LoginCommand = new Command(OnLogin, ValidateSave);
            ForgotPWCommand = new Command(OnForgotPW);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_username)
                && !String.IsNullOrWhiteSpace(_password);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public Command LoginCommand { get; }
        public Command ForgotPWCommand { get; }

        private async void OnForgotPW()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnLogin()
        {
            IUser newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = Username,
                Password = Password
            };

            //Todo Har en ide om at dette er bedre at oprette en ny database hver eneste gnag den skal bruges.
            var validateLogin =  await new ControllerDatabase(new CosmosDBService(EnumDatabase.Professionel, DateTime.Now)).ValidateLogin(newUser);
            
            //var validateLogin = await controllerDatabase.ValidateLogin(newUser);

            if (validateLogin == true)
            {
                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync($"//{nameof(RecordingsView)}");
            }

            //TODO WHAT WILL HAPPEN IF ITS WRONG???

         
        }
    }
}
