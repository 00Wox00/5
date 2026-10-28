using System.Windows;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new WorkoutListPage());
        }
    }
}