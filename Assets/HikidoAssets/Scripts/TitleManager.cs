using hikido;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hikido
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private RankingManager _rankingManager;

        [SerializeField] private float delayTime = 1.0f;
        private void Start()
        {
            //ゲームスタート時にBGM開始
            soundManager.StartBGMTIlte();
            //GameManager.ResetScore();
            //テスト用ランキングリセット
            //_rankingManager.ResetRanking();
            _rankingManager.RankingDisplay();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //soundManager.BGMStop();
            }
        }

    }
}

