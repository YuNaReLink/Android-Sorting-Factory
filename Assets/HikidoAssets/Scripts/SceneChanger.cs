using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace hikido
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private float changeTime = 1.0f;
        public string sceneName;

        //画面遷移
        public void ChangeGameScene(string sceneName)
        {
            StartCoroutine(ChangeScene(sceneName));
        }

        
        private IEnumerator ChangeScene(string sceneName)
        {
            yield return changeTime;
            SceneManager.LoadScene(sceneName);
        }  


        //画面遷移のトランジション
        private void ChangeSceneTransition()
        {
            //TODO: 画面遷移のトランジション
        }

    }
}

