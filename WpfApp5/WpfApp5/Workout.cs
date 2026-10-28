using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace WpfApp5
{
    public class Workout : INotifyPropertyChanged
    {
        private int _id = 0;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "Тренировка";
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

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type = "Силовая";
        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Exercise> Exercises { get; set; } = new();

        public int WorkoutDurationMinutes
        {
            get
            {
                int sum = 0;
                foreach (Exercise ex in Exercises)
                {
                    sum = sum + ex.ExerciseDurationMinutes;
                }
                return sum;
            }
        }

        public void Refresh()
        {
            OnPropertyChanged("WorkoutDurationMinutes");
        }

        public static string GetFileName(int id)
        {
            return "workout_" + id + ".json";
        }

        public static int GenerateId()
        {
            int id = 1;
            while (File.Exists(GetFileName(id)))
            {
                id++;
            }
            return id;
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(GetFileName(Id), json);
        }

        public void Delete()
        {
            if (File.Exists(GetFileName(Id)))
            {
                File.Delete(GetFileName(Id));
            }
        }

        public static ObservableCollection<Workout> LoadAll()
        {
            ObservableCollection<Workout> list = new();
            string[] files = Directory.GetFiles(".", "workout_*.json");

            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                Workout? workout = JsonSerializer.Deserialize<Workout>(json);
                if (workout != null)
                {
                    list.Add(workout);
                }
            }
            return list;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}