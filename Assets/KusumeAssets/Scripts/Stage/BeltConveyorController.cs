using CreateScript;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace Kusume
{
    public class BeltConveyorController : MonoBehaviour
    {
        private static BeltConveyorController   instance;
        public static BeltConveyorController    Instance => instance;

        private RobotSpawner                    robotSpawner;

        private Impact                          impact;

        private BGScroll[]                      scrolls;

        private bool                            stop;
        public bool                             Stop => stop;
        public void SetStop(bool s) {  stop = s; }

        private void Awake()
        {
            instance = this;

            impact = GetComponentInChildren<Impact>();

            robotSpawner = GetComponentInChildren<RobotSpawner>();

            scrolls = GetComponentsInChildren<BGScroll>();
        }

        private void Start()
        {
            scrolls[0].SetScrollSpeed(GameLevelManager.AndroidSpeeds[(int)GameLevelManager.GameLevel]);
            scrolls[1].SetScrollSpeed(-GameLevelManager.AndroidSpeeds[(int)GameLevelManager.GameLevel]);
        }

        public void AllMoveActivate(bool flag)
        {
            foreach(Transform robot in robotSpawner.RobotsParent)
            {
                RobotMove robotMove = robot.GetComponent<RobotMove>();
                robotMove.enabled = flag;
                robotSpawner.SetMoveFlag(flag);
                if (!robotMove.enabled)
                {
                    robotMove.Stop();
                }
            }
            for(int i = 0; i < scrolls.Length; i++)
            {
                scrolls[i].SetScrollStop(flag);
            }
        }

        public void LongCrush()
        {
            impact.LongCrush();
        }

    }
}
