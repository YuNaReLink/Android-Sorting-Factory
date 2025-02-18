using hikido;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace hikido
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gameManagerSO;
        [SerializeField] private SceneChanger sceneChanger;

        [SerializeField] private int score = 0;
        [SerializeField] private int totalScore = 0;

        //TODO: 間違いごとにスコアを変えるようにする
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;

        [Header("スコア用フラグ")]
        private bool endFlg = false;
        private bool successFlg = false;

        //スタート時にActionに関数を設定
        private void Start()
        {

        }

        private void IngameStart()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Actionに含んだ関数の使用(音源の再生、シーン切り替え）

            }
        }



        /// <summary>　/// ゲーム終了時の処理　/// </summary>
        private void EndGgme()
        {
            //条件（体力 = 0)
                //BGMの停止と切り替え

                //スコアの保存

                //次のsceneへの遷移

        }

        /// <summary> /// スコアの加算 /// </summary>
        private void ScoreUP()
        {
            if (gameManagerSO.Ingameflg)
            {
                while (!endFlg)
                {
                    totalScore += timeUpScore;
                }
                //TODO: ベルトコンベアの処理を仮で作成してからテスト
                //アンドロイドをはじくとスコア加算
                //条件文
                totalScore += upScore;
            }
                
        }





    }

}
