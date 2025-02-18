using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace hikido
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private float changeTime = 1.0f;


        //‰æ–Ê‘JˆÚ
        public void ChangeGameScene(string sceneName)
        {
            StartCoroutine(ChangeScene(sceneName));
        }

        
        private IEnumerator ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            yield return changeTime;
        }  

    }
}

