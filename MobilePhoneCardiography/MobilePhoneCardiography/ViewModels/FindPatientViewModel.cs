using MobilePhoneCardiography.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MobilePhoneCardiography.Views;
using Xamarin.Forms;

namespace MobilePhoneCardiography.ViewModels
{
    public class FindPatientViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;

        public FindPatientViewModel()
        {
            FindPatientCommand = new Command(OnSearch, ValidateBlankEntry);
            ForgotPWCommand = new Command(OnForgotPW);
            this.PropertyChanged +=
                (_, __) => FindPatientCommand.ChangeCanExecute();
        }

        private bool ValidateBlankEntry()
        {
            return !String.IsNullOrWhiteSpace(_firstName)
                && !String.IsNullOrWhiteSpace(_lastName);
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

        public Command FindPatientCommand { get; }
        public Command ForgotPWCommand { get; }

        private async void OnForgotPW()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSearch()
        {
            User newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = FirstName,
                Password = LastName
            };

            await DataStoreUser.AddItemAsync(newUser);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(RecordingsView)}");
        }
    }
}
