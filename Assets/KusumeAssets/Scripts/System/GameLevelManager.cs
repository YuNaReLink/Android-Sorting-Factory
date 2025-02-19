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

    }
}
