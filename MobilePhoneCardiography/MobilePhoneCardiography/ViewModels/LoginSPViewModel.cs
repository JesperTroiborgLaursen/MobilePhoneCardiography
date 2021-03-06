﻿
using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using DataAccessLayer;
using DataAccessLayer.Services;
using DTOs;
using EventArgss;
using MobilePhoneCardiography.Services.DataStore;
using MobilePhoneCardiography.Views;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{
    public class LoginSPViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private bool _wrongPasswordLabelVisible;
        
        public event EventHandler<UserChangedEventArgs> UserChangedEvent;
        
        public LoginSPViewModel()
        {
            
            LoginCommand = new Command(OnLogin, ValidateLoginNotBlank);
            ForgotPWCommand = new Command(OnForgotPW);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
            WrongPasswordLabelVisible = false;
        }

        private bool ValidateLoginNotBlank()
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

        public bool WrongPasswordLabelVisible
        {
            get => _wrongPasswordLabelVisible;
            set => SetProperty(ref _wrongPasswordLabelVisible, value);
        }

        public Command LoginCommand { get; }
        public Command ForgotPWCommand { get; }

        private async void OnForgotPW()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public void OnUserChange(UserChangedEventArgs e)
        {
            UserChangedEvent?.Invoke(this, e);
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
            //TODO Appen craasher hvis logn er forkert
            try
            {
                //todo real
                // var validateLogin = await new ControllerDatabase(new CosmosDBService(EnumDatabase.Professionel, DateTime.Now)).ValidateLogin(newUser);
                //Mock 
                var validateLogin = await new ControllerDatabase(new MockCosmosUser()).ValidateLogin(newUser);
                if (validateLogin)
                {
                    OnUserChange(new UserChangedEventArgs() { CurrentUser = newUser });
                    // This will pop the current page off the navigation stack
                    await Shell.Current.GoToAsync($"//{nameof(RecordingsView)}");
                    WrongPasswordLabelVisible = false;
                    Username = "";
                    Password = "";
                }
                else
                {
                    WrongPasswordLabelVisible = true;
                }
            }
            catch (Exception e)
            {
                WrongPasswordLabelVisible = true;
                Console.WriteLine(e);
                throw;

            }
            
            
            //var validateLogin = await controllerDatabase.ValidateLogin(newUser);

            

            //TODO WHAT WILL HAPPEN IF ITS WRONG???

         
        }
    }
}
