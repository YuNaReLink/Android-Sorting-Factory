using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hikido
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gamemanagerso;
        [SerializeField] private AudioManager audiomanager;

        /// <summary> /// スタート時にActionに関数を格納 /// </summary>
        private void Start()
        {
            gamemanagerso.IngameStart += StartIngameBGM;
            gamemanagerso.OutGame += OutGameBGM;
        }

        private void StartBGMTIlte()
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

        private void BGMStop()
        {
            audiomanager.BGMStop();
        }

        
        
    }
}

