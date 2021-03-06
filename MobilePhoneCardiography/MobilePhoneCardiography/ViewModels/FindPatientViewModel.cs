﻿using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    public class FindPatientViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _socSec;
        private string _socSecSearch;
        private bool confirmVisible;
        private bool cancelVisible;
        private bool findPatientVisible = true;
        private bool consentVisible;
        private string consentFrameOpacity = "0";
        private ControllerDatabase controllerDatabase;
        public bool IsProUser { get; set; }
        public FindPatientViewModel()
        {
            controllerDatabase = new ControllerDatabase(new CosmosDBService(EnumDatabase.Patient, DateTime.Now));
            FindPatientCommand = new Command(OnSearch, ValidateBlankEntry);
            CancelCommand = new Command(Cancel);
            ConfirmCommand = new Command(Confirm);
            this.PropertyChanged +=
                (_, __) => FindPatientCommand.ChangeCanExecute();
            IsProUser = false;
        }

        private async void Confirm()
        {

            //Load Measurements for patient


            //Clear entries
            SocSecSearch = "";
            SocSec = "";
            FirstName = "";
            LastName = "";
            ConsentVisible = false;
            ConsentFrameOpacity = "0";
            ToggleButtons();

            //Change view to recordings view
            await Shell.Current.GoToAsync($"//{nameof(RecordingsView)}");
        }

        private bool ValidateBlankEntry()
        {
            return !String.IsNullOrWhiteSpace(_socSecSearch);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string SocSecSearch
        {
            get => _socSecSearch;
            set => SetProperty(ref _socSecSearch, value);
        }

        public string SocSec
        {
            get => _socSec;
            set => SetProperty(ref _socSec, value);
        }

        public bool ConfirmVisible
        {
            get => confirmVisible;
            set => SetProperty(ref confirmVisible, value);
        }

        public bool CancelVisible
        {
            get => cancelVisible;
            set => SetProperty(ref cancelVisible, value);
        }

        public bool FindPatientVisible
        {
            get => findPatientVisible;
            set => SetProperty(ref findPatientVisible, value);
        }

        public bool ConsentVisible
        {
            get => consentVisible;
            set => SetProperty(ref consentVisible, value);
        }
        public string ConsentFrameOpacity
        {
            get => consentFrameOpacity;
            set => SetProperty(ref consentFrameOpacity, value);
        }

        public Command FindPatientCommand { get; }
        public Command ConfirmCommand { get; }
        public Command CancelCommand { get; }



        private void ToggleButtons()
        {
            if (findPatientVisible)
            {
                FindPatientVisible = false;
                ConfirmVisible = true;
                CancelVisible = true;
            }
            else
            {
                FindPatientVisible = true;
                ConfirmVisible = false;
                CancelVisible = false;
            }
        }

        private void Cancel()
        {
            SocSecSearch = "";
            SocSec = "";
            FirstName = "";
            LastName = "";
            ConsentVisible = false;
            ConsentFrameOpacity = "0";
            ToggleButtons();
        }

        private async void OnSearch()
        {
            ConsentVisible = true;
            ConsentFrameOpacity = "100";
            ToggleButtons();
            IPatient newPatient = new Patient()
            {
                Id = Guid.NewGuid().ToString(),
                SocSec = SocSecSearch

            };

            try
            {
                //todo har en ide om at det er bedre at oprette databasen hver gang.
                //var validatePatient = await new ControllerDatabase(new CosmosDBService(EnumDatabase.Patient, DateTime.Now)).ValidatePatient(newPatient);
                var validatePatient = await new ControllerDatabase(new MockCosmosPatient()).ValidatePatient(newPatient);
                if (validatePatient == true)
                {
                    SocSec = SocSecSearch;
                    FirstName = newPatient.FirstName;
                    LastName = newPatient.LastName;

                    //await DataStoreUser.AddItemAsync(newUser);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //var validatePatient = await controllerDatabase.ValidatePatient(newPatient);

            // This will pop the current page off the navigation stack
            //TODO DENNE SKAL IKKE VISES; ELLERS KAN MAN IKKE TJEKKE OM DET ER DEN KORREKTE PATIENT

            //await Shell.Current.GoToAsync($"//{nameof(RecordingsView)}");
        }

        public void HandleUserChangedEvent(object sender, UserChangedEventArgs e)
        {
            if (e.CurrentUser.Id != "0")
            {
                IsProUser = true;
            }
        }
    }
}
