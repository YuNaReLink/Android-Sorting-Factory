using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace hikido
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gamemanagerso;
        [SerializeField] private AudioManager audiomanager;

        [SerializeField] AudioSource audioSource;

        /// <summary> /// スタート時にActionに関数を格納 /// </summary>
        private void Start()
        {
            audiomanager = GetComponent<AudioManager>();    
            gamemanagerso.IngameStart += StartIngameBGM;
            gamemanagerso.OutGame += OutGameBGM;
            
           
        }

        private void OnEnable()
        {
            gamemanagerso.ScrapNormal += CharacterScrap_normal;
            gamemanagerso.ScrapBad += CharacterScrap_Bad;
            gamemanagerso.ScrapDestoroyer += CharacterScrap_Destoroyer;
            gamemanagerso.PushLever += PushLever;
            gamemanagerso.PullLever += PullLever;
            gamemanagerso.CheckSE += SelectCheckSE;
            gamemanagerso.DamageSE += PlayerDamageSE;   
         

        }

        private void OnDisable()
        {
            gamemanagerso.PushLever -= PushLever;
            gamemanagerso.PullLever -= PullLever;
            gamemanagerso.ScrapNormal -= CharacterScrap_normal;
            gamemanagerso.ScrapNormal -= CharacterScrap_Destoroyer;
            gamemanagerso.ScrapBad -= CharacterScrap_Bad;
            gamemanagerso.CheckSE -= SelectCheckSE;
            gamemanagerso.DamageSE -= PlayerDamageSE;
        }



        public void StartBGMTIlte()
        {
            //エラーが発生しているため
            AudioManager.Instance.PlayBGM(BGMSound.BGMDETA.Title);
        }

        
        private void StartIngameBGM()
        {
            AudioManager.Instance.PlayBGM(BGMSound.BGMDETA.Ingame);
            //エラー回避のため削除
            gamemanagerso.IngameStart -= StartIngameBGM;
        }

        private void OutGameBGM()
        {
            AudioManager.Instance.PlayBGM(BGMSound.BGMDETA.Result);
            gamemanagerso.OutGame -= OutGameBGM;
        }


       

        /// <summary> /// システム(ボタンチェック時のSE) /// </summary>
        public static void SelectCheckSE()
        {
            AudioManager.Instance.PlayspecificSE("System", 0);
        }

        /// <summary> /// レバー押す時のSE /// </summary>
        private void PushLever()
        {
            AudioManager.Instance?.PlayspecificSE("Lever_SE", 1);
            
        }

        /// <summary> /// レバー引く(戻す)ときのSE /// </summary>
        private void PullLever()
        {
            AudioManager.Instance?.PlayspecificSE("Lever_SE",0);
            
        }

        /// <summary> /// normalのロボットを破壊したとき /// </summary>
        private void CharacterScrap_normal()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 2);
        }

        /// <summary> /// normalのロボットを破壊したとき /// </summary>
        private void CharacterScrap_Destoroyer()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 0);
        }

        /// <summary> /// normalのロボットを破壊したとき /// </summary>
        private void CharacterScrap_Bad()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 1);
        }

        /// <summary> /// BGMストップ /// </summary>
        public void BGMStop()
        {
            audiomanager.BGMStop();
        }

        /// <summary>  /// プレイヤーがダメージを食らった時のSE　/// </summary>
        private void PlayerDamageSE()
        {
            AudioManager.Instance?.PlayspecificSE("System", 1);
        }

    }
}

