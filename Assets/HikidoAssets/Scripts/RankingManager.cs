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

        /// <summary> /// スコアをplayerprefsに保存 /// </summary>
        /// <param name="score"></param>
        public void SaveScore(int score)
        {
            //現在のランキングを取得しリストに格納
            List<int> scores = new List<int>();

            //rankingの数-> rankingValue
            for (int i = 1; i <= rankingValue; i++)
            {
                int cuurentScore = PlayerPrefs.GetInt("Rank" + i, 0);
                scores.Add(cuurentScore);
            }

            //新しいスコアを追加
            scores.Add(score);

            //ソート（降順）
            scores.Sort((a,b) => b.CompareTo(a));

            //rankingに保存
            for(int i = 1; i <= rankingValue; i++)
            {
                PlayerPrefs.SetInt("Rank" + i, scores[i - 1]);
                Debug.Log("Rank " + i + ": " + scores[i - 1]);  // デバッグ用に保存するスコアを表示
            }

            //保存
            PlayerPrefs.Save();
        }

        /// <summary> /// rankingをテキストで表示 /// </summary>
        public void RankingDisplay()
        {
            for (int i = 1; i <= rankingValue; i++)
            {
                int score = PlayerPrefs.GetInt("Rank" + i, 0);

                // スコアが0の場合は「0」を表示
                string displayScore = score == 0 ? "0" : score.ToString();

                //uilabelのtextにランキングを表示
                switch (i)
                {
                    //1位
                    case 1:
                        uilabel.RankingTop_1.text = displayScore;
                        break;
                    //２位
                    case 2:
                        uilabel.RankingTop_2.text = displayScore;
                        break;
                    //3位
                    case 3:
                        uilabel.RankingTop_3.text = displayScore;
                        break;
                }
            }
        }

        /// <summary> /// rankingリセット /// </summary>
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

