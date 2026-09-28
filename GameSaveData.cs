using System;
using System.Collections.Generic;
using System.Text;

namespace idle_game
{
    class GameSaveData
    {
        public double RosePetals { get; set; }
        public double PetalsPerSecond { get; set; }

        public int WateringCansPurchased { get; set; }

        public bool BeeKeeperUnlocked { get; set; }
        public int BeeKeeperProductions { get; set; }

        public DateTime SavedAt { get; set; }
    }
}
