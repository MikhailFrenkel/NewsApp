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
        private bool _userEmailVisible;
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

        public bool UserEmailVisible
        {
            get => _userEmailVisible;
            set
            {
                if (_userEmailVisible != value)
                {
                    _userEmailVisible = value;
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
                    IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
                    if (_logInOut == "Login")
                    {
                        AuthenticationResult ar = await App.PCA.AcquireTokenAsync(Constants.B2C.Scopes, App.UiParent);
                        UpdateUserInfo(ar);
                        UpdateSignInState(true);
                    }
                    else
                    {
                        while (accounts.Any())
                        {
                            await App.PCA.RemoveAsync(accounts.FirstOrDefault());
                            accounts = await App.PCA.GetAccountsAsync();
                        }
                        UpdateSignInState(false);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("AADB2C90118"))
                        OnPasswordReset();
                }
            }
            else
            {
                if (_output != null)
                {
                    await _output("Internet", "No internet connection", "OK");
                }
            }
        });

        public MasterViewModel(ShowMessageDelegate output)
        {
            InitializeTopics();
            _output = output;
            _logInOut = "Login";
            _userEmail = "Hello, user:)";
            _userEmailVisible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeTopics()
        {
            _topics = new ObservableCollection<Topic>()
            {
                new Topic{ GroupName = "Continent", Name = "Africa"},
                new Topic{ GroupName = "Continent", Name = "Asia"},
                new Topic{ GroupName = "Continent", Name = "Australia"},
                new Topic{ GroupName = "Continent", Name = "Europe"},
                new Topic{ GroupName = "Continent", Name = "North America"},
                new Topic{ GroupName = "Continent", Name = "South America"},
                new Topic{ GroupName = "More topics", Name = "Entertainment" },
                new Topic{ GroupName = "More topics", Name = "Sport" },
                new Topic{ GroupName = "More topics", Name = "Tech" },
                new Topic{ GroupName = "More topics", Name = "Business" },
                new Topic{ GroupName = "More topics", Name = "Football" }
            };
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        private void UpdateUserInfo(AuthenticationResult ar)
        {
            JObject user = ParseIdToken(ar.IdToken);
            UserEmail = "Hello, " + user["emails"]?[0];
        }

        private JObject ParseIdToken(string idToken)
        {
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        private async void OnPasswordReset()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AuthenticationResult ar = await App.PCA.AcquireTokenAsync(Constants.B2C.Scopes, (IAccount)null,
                        UIBehavior.SelectAccount, string.Empty, null, Constants.B2C.Reset, App.UiParent);
                    UpdateUserInfo(ar);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                if (_output != null)
                {
                    await _output("Internet", "No internet connection", "OK");
                }
            }
        }

        private void UpdateSignInState(bool isSignedIn)
        {
            LogInOutText = isSignedIn ? "Logout" : "Login";
            UserEmailVisible = isSignedIn;
            if (!isSignedIn)
                UserEmail = "Hello, user:)";
        }
    }
}
