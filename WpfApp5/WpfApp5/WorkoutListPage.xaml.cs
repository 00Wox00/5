using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Serialization;

namespace WpfApp5
{
    public partial class WorkoutListPage : Page
    {
        public ObservableCollection<Workout> Workouts { get; set; }

        public Workout? SelectedWorkout { get; set; }

        public WorkoutListPage()
        {
            Workouts = Workout.LoadAll();
            InitializeComponent();
            DataContext = this;
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkoutFormPage(Workouts));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new WorkoutFormPage(Workouts, SelectedWorkout));
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Тренировка не выбрана");
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
                SelectedWorkout.Delete();
                Workouts.Remove(SelectedWorkout);
            }
        }
    }
}