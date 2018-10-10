using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace NewsAppWithMvvmCross.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private string _buttonText;
        private bool _buttonVisible;

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    RaisePropertyChanged(() => ButtonText);
                }
            }
        }

        public bool ButtonVisible
        {
            get => _buttonVisible;
            set
            {
                if (_buttonVisible != value)
                {
                    _buttonVisible = value;
                    RaisePropertyChanged(() => ButtonVisible);
                }
            }
        }

        public FirstViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _buttonText = "Go to second page";
            _buttonVisible = true;
        }

        public ICommand SecondPage => new MvxAsyncCommand<string>(Button_OnClicked);

        private async Task ShowSecondPage()
        {
            await _navigationService.Navigate<SecondViewModel>();
        }

        private async Task Button_OnClicked(string e)
        {
            await _navigationService.Navigate<SecondViewModel>();
        }
    }
}
