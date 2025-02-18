using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: リファクタリングする
namespace hikido
{
    public class HPManager : MonoBehaviour
    {
        [SerializeField] private int CharactorHP = 0;
        [SerializeField] private GameObject HeartObj;

        /// <summary> /// リストとして管理 /// </summary>
        private List<GameObject> playerHeartList = new List<GameObject>();

        private void Start()
        {
            CreateHeart();
        }

        /// <summary> /// ゲーム開始時にハートを生成 /// </summary>
        private void CreateHeart()
        {
            // 既存のハートアイコンをすべて削除
            foreach (var heart in playerHeartList)
            {
                Destroy(heart);
            }
            playerHeartList.Clear();

            // 最大体力分のハートアイコンを生成
            for (int i = 0; i < CharactorHP; i++)
            {
                GameObject heart = Instantiate(HeartObj);
                heart.transform.parent = transform;
                playerHeartList.Add(heart);
            }
        }

        /// <summary> /// ハートの表示・非表示 /// </summary>
        private void AstrengthDisplay()
        {
            // すべてのハートアイコンを非表示にする
            foreach (var heart in playerHeartList)
            {
                heart.SetActive(false);
            }

            // 現在の体力分のハートアイコンを表示
            for (int i = 0; i < CharactorHP; i++)
            {
                if (i < playerHeartList.Count) // 配列の範囲内か確認
                {
                    playerHeartList[i].SetActive(true);
                }
            }
        }

    }

}
