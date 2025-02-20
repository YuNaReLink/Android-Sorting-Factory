using hikido;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace hikido
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameManagerSO gameManagerSO;
        [SerializeField] private SceneChanger sceneChanger;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HPManager hpManager;
        [SerializeField] private RankingManager _rankingManager;


        [Header("スコア")]
        //TODO: 間違いごとにスコアを変えるようにする
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;

        //合計スコア
        public static int totalScore = 0;


        //生存時間用のタイム
        public static float aliveTime = 0;



        [Header("スコア用フラグ")]
        private bool endFlg = false;

        /*ここから追加しました（by楠目）*/
        [Header("スコア用UI")]
        [SerializeField]
        private Text ScoreText;

        private static int  normalAndroidNumber;

        private static int  badAndroidNumber;

        public static int   NormalAndroidNumber => normalAndroidNumber;

        public static void AddNormalAndroidNumber() {  normalAndroidNumber++; }

        public static int   BadAndroidNumber => badAndroidNumber;
        public static void AddBadAndroidNumber() { badAndroidNumber++; }

        public static int TotalScore => totalScore;

        public static float AliveTime => aliveTime;

        private void Awake()
        {
            soundManager = FindObjectOfType<SoundManager>();
            sceneChanger = FindObjectOfType<SceneChanger>();
            hpManager = FindObjectOfType<HPManager>();
            _rankingManager = FindObjectOfType<RankingManager>();
        }

        //スタート時にActionに関数を設定
        private void Start()
        {
            //暗転解除
            if (Fade.Instance != null)
            {

                StartCoroutine(Fade.Instance.FadeIn(0, 1f));
            }

            //一度だけ最初に呼び出す
            InvokeRepeating("TimeCountUP", 0.0f, 1.0f);
            IngameStart();

            totalScore = 0;
            aliveTime = 0;
        }

        private void Update()
        {
            CountAliveTime();
        }

        //生存時間のカウントをする
        private void CountAliveTime()
        {
            aliveTime += Time.deltaTime;
        }

        private void OnEnable()
        {
            gameManagerSO.OnAddScore += ScoreUP;
            gameManagerSO.OutGame += EndGgme;
        }

        private void OnDisable()
        {
            gameManagerSO.OnAddScore -= ScoreUP;
            gameManagerSO.OutGame -= EndGgme;
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
                //soundManager.BGMStop();

                //次のsceneへの遷移
                //TODO;数秒間だけずらすようにする
                //SceneManager.LoadScene("ResultScene");

                //scoreの保存
                _rankingManager.SaveScore(totalScore);
                Debug.Log(totalScore + "現在のスコア ");

                //aliveTimeの保存
                _rankingManager.SaveAliveTime(aliveTime);
                Debug.Log(aliveTime + "現在のタイム ");

                //test
                //SceneManager.LoadScene("00_TitleScene_hikido");

            }
        }


        /// <summary> /// 時間経過によるスコアの加算 /// </summary>
        private void TimeCountUP()
        {
            //totalScore += timeUpScore;
            //uIlabel.ScoreUP.text = totalScore.ToString();
            //Debug.Log(totalScore);
        }

        /// <summary> /// スコアの加算 /// </summary>
        private void ScoreUP()
        {
            //totalScore += upScore;
            //ingame時のみスコアを加算
            //enumでキャラクタータイプを設定している
            //アンドロイドをはじくとスコア加算
            //条件文
            totalScore += upScore;
            /*
            else if(endFlg) 
            {
                //時間経過によるスコア加算を止める
                CancelInvoke();

            }
             */
            ScoreUI(totalScore);
        }

        private void ScoreUI(int score)
        {
            ScoreText.text = totalScore.ToString();
        }

        /// <summary> /// スコアを初期化 /// </summary>
        public void ResetScore()
        {
            totalScore = 0;
        }
        
    }

}
