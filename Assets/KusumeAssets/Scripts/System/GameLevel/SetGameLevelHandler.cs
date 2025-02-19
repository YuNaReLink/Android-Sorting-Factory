using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    public class SetGameLevelHandler : MonoBehaviour
    {

        [SerializeField]
        private GameLevel gameLevel;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(SetLevel);
        }


        private void SetLevel()
        {
            GameLevelManager.SetGameLevel(gameLevel);
        }
    }
}
