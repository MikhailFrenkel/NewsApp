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
    public class MasterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private List<string> _topics;

        public List<string> Topics
        {
            get => _topics;
            set
            {
                if (_topics != value)
                {
                    _topics = value;
                    RaisePropertyChanged(() => Topics);
                }
            }
        }

        public ICommand ItemTappedCommand => new MvxAsyncCommand<string>(ItemTapped);

        public MasterViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _topics = Constants.Topics;
        }

        private async Task ItemTapped(string topic)
        {
        }
    }
}
