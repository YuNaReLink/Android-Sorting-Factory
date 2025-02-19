using hikido;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hikido
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private SceneChanger changer;
        [SerializeField] private SoundManager soundManager;

        [SerializeField] private float delayTime = 1.0f;
        private void Start()
        {
            //ゲームスタート時にBGM開始
            Invoke("soundManager.StartBGMTIlte", delayTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                soundManager.BGMStop();
            }
        }

    }
}

