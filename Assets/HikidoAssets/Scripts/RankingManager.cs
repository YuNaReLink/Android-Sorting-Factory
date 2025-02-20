using hikido;
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

        /// <summary> /// ranking���e�L�X�g�ŕ\�� /// </summary>
        public void RankingDisplay()
        {
            for (int i = 1; i <= rankingValue; i++)
            {
                int score = PlayerPrefs.GetInt("Rank" + i, 0);

                // �X�R�A��0�̏ꍇ�́u0�v��\��
                string displayScore = score == 0 ? "0" : score.ToString();

                //uilabel��text�Ƀ����L���O��\��
                switch (i)
                {
                    //1��
                    case 1:
                        uilabel.RankingTop_1.text = displayScore;
                        break;
                    //�Q��
                    case 2:
                        uilabel.RankingTop_2.text = displayScore;
                        break;
                    //3��
                    case 3:
                        uilabel.RankingTop_3.text = displayScore;
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
            }
            PlayerPrefs.Save();
        }
    }
}

