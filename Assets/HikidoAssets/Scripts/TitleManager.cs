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
            //�Q�[���X�^�[�g����BGM�J�n
            soundManager.StartBGMTIlte();
            //GameManager.ResetScore();
            //�e�X�g�p�����L���O���Z�b�g
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

