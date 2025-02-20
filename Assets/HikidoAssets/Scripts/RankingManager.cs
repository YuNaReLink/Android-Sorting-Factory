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


        //時間変換用

        int min = 0;
        int second = 0;
        int comma = 0;

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

        public void SaveAliveTime(float aliveTime)
        {
            //現在のランキングを取得しリストに格納
            List<float> aliveTimes = new List<float>();

            //rankingの数-> rankingValue
            for (int i = 1; i <= rankingValue; i++)
            {
                float currentAliveTime = PlayerPrefs.GetFloat("RankTime" + i, 0);
                aliveTimes.Add(currentAliveTime);
            }

            //新しいスコアを追加
            aliveTimes.Add(aliveTime);

            //ソート（降順）
            aliveTimes.Sort((a, b) => b.CompareTo(a));

            //rankingに保存
            for (int i = 1; i <= rankingValue; i++)
            {
                PlayerPrefs.SetFloat("RankTime" + i, aliveTimes[i - 1]);
                Debug.Log("RankTime" + i + ": " + aliveTimes[i - 1]);  // デバッグ用に保存するスコアを表示
            }

            //保存
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 秒数.少数秒　⇒　分、秒数、少数秒に直す　(123.5 ＝＞02:03.05)
        /// </summary>
        /// <param name="aliveTime"></param>
        private void AliveTimeCalculate(float aliveTime)
        {
            //小数がある数a - 小数を無視した数a = aの小数点以下部分が得られる？

            min = ((int)aliveTime / 60);
            aliveTime = Mathf.FloorToInt(aliveTime % 60);
            //aliveTime = aliveTime * 60;

            //second = ((int)aliveTime / 60);
            //aliveTime = aliveTime % 1;

            //comma = (int)aliveTime;
        }

        /// <summary> /// rankingをテキストで表示 /// </summary>
        public void RankingDisplay()
        {
            for (int i = 1; i <= rankingValue; i++)
            {
                int score = PlayerPrefs.GetInt("Rank" + i, 0);
                float aliveTime = PlayerPrefs.GetFloat("RankTime" + i, 0);

                
              AliveTimeCalculate(aliveTime);

                // スコアが0の場合は「0」を表示
                string displayScore = (score == 0)?"0":score.ToString();
                string displayAliveTime = (aliveTime == 0)?"0":aliveTime.ToString("F2");

                //uilabelのtextにランキングを表示
                switch (i)
                {
                    //1位
                    case 1:
                        uilabel.RankingTop_1.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_1.text = (min +":" + displayAliveTime);              
                            break;
                    //２位
                    case 2:
                        uilabel.RankingTop_2.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_2.text = (min + ":" + displayAliveTime);
                        break;
                    //3位
                    case 3:
                        uilabel.RankingTop_3.text = displayScore;
                        uilabel.Ranking_AliveTimeTop_3.text = displayAliveTime;
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
                PlayerPrefs.SetFloat("RankTime" + i, 0);
            }
            PlayerPrefs.Save();
        }
    }
}

