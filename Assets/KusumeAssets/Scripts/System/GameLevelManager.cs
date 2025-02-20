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
            4,
            5
        };

        public static float[] AndroidSpeeds => androidSpeeds;

        private static float[] spawnInterval = new float[]
        {
            2f,
            2.1f,
            2.25f
        };

        public static float[] SpawnInterval => spawnInterval;
    }
}
