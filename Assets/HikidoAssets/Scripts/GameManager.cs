using hikido;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace hikido
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameManagerSO gameManagerSO;
        [SerializeField] private SceneChanger sceneChanger;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HPManager hpManager;

        //仮
        [SerializeField] private UIlabel uIlabel;


        [Header("スコア")]
        //TODO: 間違いごとにスコアを変えるようにする
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;
        public static int highScore = 0;
        public static int totalScore = 0;
        

        //仮のキャラクターHP
        [SerializeField] private int CharactorHP = 0;


        [Header("スコア用フラグ")]
        private bool endFlg = false;
        private bool successFlg = false;


        //スタート時にActionに関数を設定
        private void Start()
        {
            //一度だけ最初に呼び出す
            InvokeRepeating("TimeCountUP", 0.0f, 1.0f);

        }

        private void Update()
        {
            //終了フラグが立つまで
            if (!endFlg)
            {
                ScoreUP();
            }
        }

        /// <summary>　/// 難易度選択後　/// </summary>
        private void IngameStart()
        {

            //gamestart時にAction内の格納している関数を使用
            gameManagerSO.IngameStart?.Invoke();
        } 


        /// <summary>　/// ゲーム終了時の処理　/// </summary>
        private void EndGgme()
        {
            //条件（体力 = 0) playerからHPを参照
            if(hpManager.EndFlg())
            {
                endFlg = true;
                gameManagerSO.OutGameflg = true;

                //BGMの停止と切り替え
                soundManager.BGMStop();

                //次のsceneへの遷移
                //SceneManager.LoadScene("ResultScene");

        
            }



        }


        /// <summary> /// 時間経過によるスコアの加算 /// </summary>
        private void TimeCountUP()
        {
            totalScore += timeUpScore;
            uIlabel.ScoreUP.text = totalScore.ToString();
            Debug.Log(totalScore);
        }

        /// <summary> /// スコアの加算 /// </summary>
        private void ScoreUP()
        {
            //ingame時のみスコアを加算
            if (gameManagerSO.Ingameflg)
            {
                //TODO: ベルトコンベアの処理を仮で作成してからテスト
                //アンドロイドをはじくとスコア加算
                //条件文
                    //totalScore += upScore;
            }
            else if(endFlg) 
            {
                //時間経過によるスコア加算を止める
                CancelInvoke();
                highScore = totalScore;
                if(highScore < totalScore)
                {
                    totalScore = highScore;
                }
            }

        }

        /// <summary> /// スコアを初期化 /// </summary>
        public void ResetScore()
        {
            totalScore = 0;
        }

       

        
    }

}
