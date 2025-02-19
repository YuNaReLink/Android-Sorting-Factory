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

        //��ʑJ��
        public void ChangeGameScene(string sceneName)
        {
            StartCoroutine(ChangeScene(sceneName));
        }

        
        private IEnumerator ChangeScene(string sceneName)
        {
            yield return changeTime;
            SceneManager.LoadScene(sceneName);
        }  


        //��ʑJ�ڂ̃g�����W�V����
        private void ChangeSceneTransition()
        {
            //TODO: ��ʑJ�ڂ̃g�����W�V����
        }

    }
}

