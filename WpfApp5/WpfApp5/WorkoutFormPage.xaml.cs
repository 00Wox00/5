using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp5
{
    public partial class WorkoutFormPage : Page
    {
        private ObservableCollection<Workout> _workoutList;
        private Workout? _originalWorkout;

        public Workout CurrentWorkout { get; set; }

        public Exercise? SelectedExercise { get; set; }

        public WorkoutFormPage(ObservableCollection<Workout> workoutList, Workout? workout = null)
        {
            _workoutList = workoutList;
            _originalWorkout = workout;
            CurrentWorkout = new Workout();

            if (workout == null)
            {
                CurrentWorkout.Id = Workout.GenerateId();
            }
            else
            {
                CurrentWorkout.Id = workout.Id;
                CurrentWorkout.Name = workout.Name;
                CurrentWorkout.Date = workout.Date;
                CurrentWorkout.Type = workout.Type;

                foreach (Exercise ex in workout.Exercises)
                {
                    Exercise copy = new Exercise();
                    copy.Name = ex.Name;
                    copy.Sets = ex.Sets;
                    copy.SetDurationMinutes = ex.SetDurationMinutes;
                    CurrentWorkout.Exercises.Add(copy);
                }
            }

            InitializeComponent();
            DataContext = this;
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (tbExName.Text == "")
            {
                MessageBox.Show("Введите название упражнения");
                return;
            }

            int sets;
            if (!int.TryParse(tbExSets.Text, out sets))
            {
                MessageBox.Show("Количество подходов нужно ввести числом");
                return;
            }

            int setDuration;
            if (!int.TryParse(tbExSetDuration.Text, out setDuration))
            {
                MessageBox.Show("Время подхода нужно ввести числом");
                return;
            }

            Exercise ex = new Exercise();
            ex.Name = tbExName.Text;
            ex.Sets = sets;
            ex.SetDurationMinutes = setDuration;

            CurrentWorkout.Exercises.Add(ex);
            CurrentWorkout.Refresh();

            tbExName.Text = "";
            tbExSets.Text = "";
            tbExSetDuration.Text = "";
        }

        private void DeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedExercise == null)
            {
                MessageBox.Show("Упражнение не выбрано");
                return;
            }

            bool confirm = MessageBox.Show(
                    "Вы действительно хотите удалить запись?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                ) == MessageBoxResult.Yes;

            if (confirm)
            {
                CurrentWorkout.Exercises.Remove(SelectedExercise);
                CurrentWorkout.Refresh();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentWorkout.Name == "")
            {
                MessageBox.Show("Введите название тренировки");
                return;
            }

            if (_originalWorkout == null)
            {
                _workoutList.Add(CurrentWorkout);
                CurrentWorkout.Save();
                BackButton_Click(sender, e);
                return;
            }

            _originalWorkout.Name = CurrentWorkout.Name;
            _originalWorkout.Date = CurrentWorkout.Date;
            _originalWorkout.Type = CurrentWorkout.Type;

            _originalWorkout.Exercises.Clear();
            foreach (Exercise ex in CurrentWorkout.Exercises)
            {
                _originalWorkout.Exercises.Add(ex);
            }
            _originalWorkout.Refresh();

            _originalWorkout.Save();
            BackButton_Click(sender, e);
        }
    }
}