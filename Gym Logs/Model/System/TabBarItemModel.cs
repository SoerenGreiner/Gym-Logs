using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    public class TabBarItemModel : INotifyPropertyChanged
    {
        private bool isSelected;

        public string Title { get; set; }
        public string Icon { get; set; }
        public TabBarActionEnum Action { get; set; }
        public bool IsVisible { get; set; } = true;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand Command { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}