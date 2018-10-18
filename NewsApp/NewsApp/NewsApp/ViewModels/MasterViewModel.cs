using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NewsApp.Models;
using Xamarin.Forms;

namespace NewsApp.ViewModels
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Topic> _topics;

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

        public MasterViewModel()
        {
            InitializeTopics();
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
    }
}
