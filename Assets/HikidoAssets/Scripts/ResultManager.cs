using hikido;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hikido
{
    public class ResultManager : MonoBehaviour
    {
        WaitForSeconds oneSec;

        [SerializeField] private GameManagerSO gameManagerSO;
        [SerializeField] private UIlabel uiLabel;
        [SerializeField] private GameManager gameManager;

        [Header("テスト用")]
        int genuineProduct = 50;
        int defectiveProduct = 3;

        private void Start()
        {
            //Actionに格納 -> result画面用のBGM再生
            gameManagerSO.OutGame?.Invoke();
            Result();
        }


        private void Result()
        {
            StartCoroutine(ResultDisplay());
        }

        //スコア表示(仮）
        private IEnumerator ResultDisplay()
        {
            yield return oneSec;

            uiLabel.ScoreUP.text = GameManager.totalScore.ToString();

            yield return oneSec ;

            uiLabel.GenuineProduct.text = genuineProduct.ToString();

            yield return oneSec;

            uiLabel.defectiveProduct.text = defectiveProduct.ToString();

        }

        //TODO:リザルト画面からタイトルに戻るときのBGMstopの処理

        
    }

}
