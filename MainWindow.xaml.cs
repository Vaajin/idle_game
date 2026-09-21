using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace idle_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double rosePetals = 0;
        private double petalsPerSecond = 1;

        private readonly DispatcherTimer gameTimer;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            UpdateUI();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            rosePetals += petalsPerSecond / 10;

            UpdateUI();
        }

        private void WaterRose_Click(object sender, RoutedEventArgs e)
        {
            rosePetals += 1;

            UpdateUI();
        }

        private void UpdateUI()
        {
            RosePetalsText.Text = $"🌹 {rosePetals:F1} Rose Petals";
            PetalsPerSecondText.Text = $"+{petalsPerSecond:F1} Petals per second";
        }
    }
}