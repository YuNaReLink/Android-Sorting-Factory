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

        /// <summary> /// �X�^�[�g����Action�Ɋ֐����i�[ /// </summary>
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


       

        /// <summary> /// �V�X�e��(�{�^���`�F�b�N����SE) /// </summary>
        public static void SelectCheckSE()
        {
            AudioManager.Instance.PlayspecificSE("System", 0);
        }

        /// <summary> /// ���o�[��������SE /// </summary>
        private void PushLever()
        {
            AudioManager.Instance?.PlayspecificSE("Lever_SE", 1);
            
        }

        /// <summary> /// ���o�[����(�߂�)�Ƃ���SE /// </summary>
        private void PullLever()
        {
            AudioManager.Instance?.PlayspecificSE("Lever_SE",0);
            
        }

        /// <summary> /// normal�̃��{�b�g��j�󂵂��Ƃ� /// </summary>
        private void CharacterScrap_normal()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 2);
        }

        /// <summary> /// normal�̃��{�b�g��j�󂵂��Ƃ� /// </summary>
        private void CharacterScrap_Destoroyer()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 0);
        }

        /// <summary> /// normal�̃��{�b�g��j�󂵂��Ƃ� /// </summary>
        private void CharacterScrap_Bad()
        {
            AudioManager.Instance?.PlayspecificSE("robots_SE", 1);
        }

        /// <summary> /// BGM�X�g�b�v /// </summary>
        public void BGMStop()
        {
            audiomanager.BGMStop();
        }

        /// <summary>  /// �v���C���[���_���[�W��H���������SE�@/// </summary>
        private void PlayerDamageSE()
        {
            AudioManager.Instance?.PlayspecificSE("System", 1);
        }

    }
}

