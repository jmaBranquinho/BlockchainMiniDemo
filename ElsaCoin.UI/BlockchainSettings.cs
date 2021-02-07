namespace ElsaCoin.UI
{
    public static class BlockchainSettings
    {
        public static int InitialDifficulty = 2;

        public static int Difficulty = InitialDifficulty;

        private static int InitialMiningReward = 10;

        public static int MiningReward
        {
            get
            {
                return InitialMiningReward * Difficulty;
            }
        }
    }
}
