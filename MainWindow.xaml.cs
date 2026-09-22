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
        private double wateringCanCost = 10;

        private double beeKeeperCost = 100;
        private bool beeKeeperUnlocked = false;
        private int beeKeeperProductions = 0;

        private readonly DispatcherTimer gameTimer;
        private readonly DispatcherTimer beeKeeperTimer;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            beeKeeperTimer = new DispatcherTimer();
            beeKeeperTimer.Interval = TimeSpan.FromSeconds(1);
            beeKeeperTimer.Tick += BeeKeeperTimer_Tick;

            UpdateUI();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            rosePetals += petalsPerSecond / 10;

            UpdateUI();
        }

        private void BeeKeeperTimer_Tick(object? sender, EventArgs e)
        {
            rosePetals += 1;
            beeKeeperProductions++;

            ProductionLogText.Text =
                $"🐝 Bee Keeper: +1 Rose Petal ×{beeKeeperProductions}\n" +
                $"Total produced: {beeKeeperProductions} Rose Petals";

            UpdateUI();
        }

        private void WaterRose_Click(object sender, RoutedEventArgs e)
        {
            rosePetals += 1;

            UpdateUI();
        }

        private void BuyWateringCan_Click(object sender, RoutedEventArgs e)
        {
            if (rosePetals >= wateringCanCost)
            {
                rosePetals -= wateringCanCost;
                petalsPerSecond += 1;

                UpdateUI();
            }
        }

        private void BuyBeeKeeper_Click(object sender, RoutedEventArgs e)
        {
            if (rosePetals >= beeKeeperCost && !beeKeeperUnlocked)
            {
                rosePetals -= beeKeeperCost;
                beeKeeperUnlocked = true;

                beeKeeperTimer.Start();

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            RosePetalsText.Text = $"🌹 {rosePetals:F1} Rose Petals";
            PetalsPerSecondText.Text = $"+{petalsPerSecond:F1} Petals per second";
            
            WateringCanButton.IsEnabled = rosePetals >= wateringCanCost;

            if (beeKeeperUnlocked)
            {
                BeeKeeperButton.IsEnabled = false;
                BeeKeeperButton.Content = "🐝 Bee Keeper Unlocked";
            }
            else
            {
                BeeKeeperButton.IsEnabled = rosePetals >= beeKeeperCost;
            }
        }
    }
}