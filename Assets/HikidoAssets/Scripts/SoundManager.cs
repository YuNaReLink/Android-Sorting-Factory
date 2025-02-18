using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hikido
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gamemanagerso;
        [SerializeField] private AudioManager audiomanager;

        /// <summary> /// �X�^�[�g����Action�Ɋ֐����i�[ /// </summary>
        private void Start()
        {
            gamemanagerso.IngameStart += StartIngameBGM;
            gamemanagerso.OutGame += OutGameBGM;
        }

        private void StartBGMTIlte()
        {
            //�G���[���������Ă��邽��
            AudioManager.Instance.PlayBGM(BGMSound.BGMDETA.Title);
        }

        
        private void StartIngameBGM()
        {
            AudioManager.Instance.PlayBGM(BGMSound.BGMDETA.Ingame);
            //�G���[����̂��ߍ폜
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

