using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp5
{
    public class Exercise : INotifyPropertyChanged
    {
        private string _name = "Отжимания";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _sets = 3;
        public int Sets
        {
            get => _sets;
            set
            {
                if (_sets != value)
                {
                    _sets = value;
                    OnPropertyChanged();
                    OnPropertyChanged("ExerciseDurationMinutes");
                }
            }
        }

        private int _setDurationMinutes = 2;
        public int SetDurationMinutes
        {
            get => _setDurationMinutes;
            set
            {
                if (_setDurationMinutes != value)
                {
                    _setDurationMinutes = value;
                    OnPropertyChanged();
                    OnPropertyChanged("ExerciseDurationMinutes");
                }
            }
        }

        public int ExerciseDurationMinutes => Sets * SetDurationMinutes;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}