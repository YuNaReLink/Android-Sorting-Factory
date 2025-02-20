namespace Kusume
{
    public static class InputManager
    {
        private static bool inputFlag = false;
        public static bool InputFlag => inputFlag;

        public static void SetInputFlag(bool flag) { inputFlag = flag; }
    }
}
