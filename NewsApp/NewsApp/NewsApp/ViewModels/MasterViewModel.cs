using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Identity.Client;
using NewsApp.Models;
using NewsApp.Resources;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace NewsApp.ViewModels
{
    public delegate Task ShowMessageDelegate(string title, string message, string cancel);

    public class MasterViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Topic> _topics;
        private string _logInOut;
        private string _userEmail;
        private readonly ShowMessageDelegate _output;

        public ObservableCollection<Topic> Topics
        {
            get => _topics;
            set
            {
                if (_topics != value)
                {
                    _topics = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LogInOutText
        {
            get => _logInOut;
            set
            {
                if (_logInOut != value)
                {
                    _logInOut = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                if (_userEmail != value)
                {
                    _userEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LogInOutCommand => new Command(async () =>
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (_logInOut == Resource.MasterPageLoginText)
                    {
                        var authenticationResult =  await App.AuthorizationService.Login();

                        UpdateUserInfo(authenticationResult);
                        UpdateSignInState(true);
                    }
                    else
                    {
                        await App.AuthorizationService.Logout();
                        UpdateSignInState(false);
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                if (_output != null)
                {
                    await _output(Resource.MasterPageNoInternetTitle, Resource.NoInternetText, Resource.MasterPageNoInternetCancel);
                }
            }
        });

        public MasterViewModel(ShowMessageDelegate output, AuthenticationResult ar)
        {
            _topics = new ObservableCollection<Topic>(Constants.Topics);
            _output = output;
            if (ar != null)
            {
                _logInOut = Resource.MasterPageLogoutText;
                _userEmail = Resource.MasterPageUserEmailText +
                             App.AuthorizationService.ParseIdToken(ar.IdToken)["emails"]?[0];
            }
            else
            {
                _logInOut = Resource.MasterPageLoginText;
                _userEmail = Resource.MasterPageNoUserEmailText;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateUserInfo(AuthenticationResult ar)
        {
            var user = App.AuthorizationService.ParseIdToken(ar.IdToken);
            UserEmail = Resource.MasterPageUserEmailText + user["emails"]?[0];
        }

        private void UpdateSignInState(bool isSignedIn)
        {
            LogInOutText = isSignedIn ? Resource.MasterPageLogoutText : Resource.MasterPageLoginText;
            if (!isSignedIn)
                UserEmail = Resource.MasterPageNoUserEmailText;
        }
    }
}
