namespace Kusume
{
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }
    public static class GameLevelManager
    {
        private static GameLevel gameLevel;

        public static GameLevel GameLevel => gameLevel;

        public static void SetGameLevel(GameLevel level) {  gameLevel = level; }

        private static float[] androidSpeeds = new float[]
        {
            3,
            5,
            7
        };

        public static float[] AndroidSpeeds => androidSpeeds;

        private static float[] spawnInterval = new float[]
        {
            2f,
            1.5f,
            1.0f
        };

        public static float[] SpawnInterval => spawnInterval;

        private static float[] minConsecutiveSpawnIntervals = new float[]
        {
            1.0f,
            0.8f,
            0.6f,
        };
        public static float[] MinConsecutiveSpawnIntervals => minConsecutiveSpawnIntervals;

        private static float[] maxConsecutiveSpawnIntervals = new float[]
        {
            1.5f,
            1.0f,
            0.8f,
        };
        public static float[] MaxConsecutiveSpawnIntervals => maxConsecutiveSpawnIntervals;
    }
}
