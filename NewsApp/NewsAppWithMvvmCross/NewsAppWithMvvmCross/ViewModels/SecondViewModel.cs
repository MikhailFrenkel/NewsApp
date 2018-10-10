using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace NewsAppWithMvvmCross.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        private string _text;
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

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged(() => Text);
                }
            }
        }

        public ICommand ItemTappedCommand => new MvxCommand<string>(ItemTapped);

        public override async Task Initialize()
        {
            await base.Initialize();

            _text = "Second Page";
            _topics = Constants.Topics;
        }

        private void ItemTapped(string item)
        {
            int a = 10;
        }
    }
}
