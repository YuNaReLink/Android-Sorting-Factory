using hikido;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace hikido
{
    public class RankingManager : MonoBehaviour
    {

        [SerializeField] private const int rankingValue = 3;
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private UIlabel uilabel;


        //���ԕϊ��p

        int min = 0;
        int second = 0;
        int comma = 0;

        /// <summary> /// �X�R�A��playerprefs�ɕۑ� /// </summary>
        /// <param name="score"></param>
        public void SaveScore(int score)
        {
            //���݂̃����L���O���擾�����X�g�Ɋi�[
            List<int> scores = new List<int>();

            //ranking�̐�-> rankingValue
            for (int i = 1; i <= rankingValue; i++)
            {
                int cuurentScore = PlayerPrefs.GetInt("Rank" + i, 0);
                scores.Add(cuurentScore);
            }

            //�V�����X�R�A��ǉ�
            scores.Add(score);

            //�\�[�g�i�~���j
            scores.Sort((a,b) => b.CompareTo(a));

            //ranking�ɕۑ�
            for(int i = 1; i <= rankingValue; i++)
            {
                PlayerPrefs.SetInt("Rank" + i, scores[i - 1]);
                Debug.Log("Rank " + i + ": " + scores[i - 1]);  // �f�o�b�O�p�ɕۑ�����X�R�A��\��
            }

            //�ۑ�
            PlayerPrefs.Save();
        }

        public void SaveAliveTime(float aliveTime)
        {
            //���݂̃����L���O���擾�����X�g�Ɋi�[
            List<float> aliveTimes = new List<float>();

            //ranking�̐�-> rankingValue
            for (int i = 1; i <= rankingValue; i++)
            {
                float currentAliveTime = PlayerPrefs.GetFloat("RankTime" + i, 0);
                aliveTimes.Add(currentAliveTime);
            }

            //�V�����X�R�A��ǉ�
            aliveTimes.Add(aliveTime);

            //�\�[�g�i�~���j
            aliveTimes.Sort((a, b) => b.CompareTo(a));

            //ranking�ɕۑ�
            for (int i = 1; i <= rankingValue; i++)
            {
                PlayerPrefs.SetFloat("RankTime" + i, aliveTimes[i - 1]);
                Debug.Log("RankTime" + i + ": " + aliveTimes[i - 1]);  // �f�o�b�O�p�ɕۑ�����X�R�A��\��
            }

            //�ۑ�
            PlayerPrefs.Save();
        }

        /// <summary>
        /// �b��.�����b�@�ˁ@���A�b���A�����b�ɒ����@(123.5 ����02:03.05)
        /// </summary>
        /// <param name="aliveTime"></param>
        private void AliveTimeCalculate(float aliveTime)
        {
            //���������鐔a - �����𖳎�������a = a�̏����_�ȉ�������������H

            min = ((int)aliveTime / 60);
            aliveTime = Mathf.FloorToInt(aliveTime % 60);
            //aliveTime = aliveTime * 60;

            //second = ((int)aliveTime / 60);
            //aliveTime = aliveTime % 1;

            //comma = (int)aliveTime;
        }

        /// <summary> /// ranking���e�L�X�g�ŕ\�� /// </summary>
        public void RankingDisplay()
        {
            for (int i = 1; i <= rankingValue; i++)
            {
                int score = PlayerPrefs.GetInt("Rank" + i, 0);
                float aliveTime = PlayerPrefs.GetFloat("RankTime" + i, 0);

                
              AliveTimeCalculate(aliveTime);

                // �X�R�A��0�̏ꍇ�́u0�v��\��
                string displayScore = (score == 0)?"0":score.ToString();
                string displayAliveTime = (aliveTime == 0)?"0":aliveTime.ToString("F2");

                //uilabel��text�Ƀ����L���O��\��
                switch (i)
                {
                    //1��
                    case 1:
                        uilabel.RankingTop_1.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_1.text = (min +":" + displayAliveTime);              
                            break;
                    //�Q��
                    case 2:
                        uilabel.RankingTop_2.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_2.text = (min + ":" + displayAliveTime);
                        break;
                    //3��
                    case 3:
                        uilabel.RankingTop_3.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_3.text = displayAliveTime;
                        break;
                }
            }
        }

        /// <summary> /// ranking���Z�b�g /// </summary>
        public void ResetRanking()
        {
            for (int i = 1; i <= rankingValue; i++)
            {
                PlayerPrefs.SetInt("Rank" + i, 0);
                PlayerPrefs.SetFloat("RankTime" + i, 0);
            }
            PlayerPrefs.Save();
        }
    }
}

