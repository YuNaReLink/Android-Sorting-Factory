using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kusume
{
    public enum SceneList
    {
        Title,
        Game
    }

    public class ChangeScene : MonoBehaviour
    {
        [SerializeField]
        private SceneList sceneList;

        private Button button;


        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(SetChangeScene);
        }

        private void SetChangeScene()
        {
            StartCoroutine(Fade.Instance.FadeOut(0, 1.5f, () =>
            {
                
            string name = "00_TitleScene";
           
                switch (sceneList)
                {
                    case SceneList.Title:
                        name = "00_TitleScene";
                        break;
                    case SceneList.Game:
                        name = "01_GameScene";
                        break;
                }

            SceneChanger.LoadScene(name);
            }
              ));

        }
    }


    public static class SceneChanger
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
