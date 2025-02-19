using JetBrains.Annotations;
using Kusume;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//TODO: ���t�@�N�^�����O����
namespace hikido
{
    public class HPManager : MonoBehaviour
    {

        //�L�����N�^�[�̍ő�̗�
        [SerializeField] private int charactorHP = 3;
        private int  currentHP = 0;
        private bool oneShotOut = false;
        private bool endFlg = false;

        [Header("HPImage")]
        [SerializeField] private Image HPImage;
        [SerializeField] private List<Sprite> hpSprite = new List<Sprite>();
        [SerializeField] private GameManagerSO _gameManagerSO;
        [SerializeField] private AndroidTypeController _controller;

        private void Start()
        {
            currentHP = charactorHP;
            UpdateHelth();
        }

        private void OnEnable()
        {
            _gameManagerSO.OnAddDamage += TakeDamage;
            _gameManagerSO.MistakeDamage += MistakeImpact;
        }

        private void OnDisable()
        {
            _gameManagerSO.OnAddDamage -= TakeDamage;
            _gameManagerSO.MistakeDamage -= MistakeImpact;
        }
        
        private void Update()
        {
            //�e�X�g�p
            //TakeDamage(1); 
        }

        public void MistakeImpact()
        {
            TakeDamage(1);
        }

        public bool EndFlg()
        {
            return endFlg;
        }
       
        //�_���[�W
        public void TakeDamage(int damage)
        {
            currentHP -= damage;
           
            //CurrentHP�̐��l�͈̔͂�����(HP��� 3,���� 0)
            currentHP = Mathf.Clamp(currentHP, 0, charactorHP);

            //�_���[�W���󂯂��Ƃ�->�̗̓o�[�ω�
            UpdateHelth();

    
            if(currentHP == 0)
            {
                endFlg = true;
                EndFlg();
            }
            
        }



        //HP�o�[�̉摜�؂�ւ�
        private void UpdateHelth()
        {
            if(HPImage == null)
            {
                Debug.LogError("Image������܂���B");
            }
            //���݂̗̑͂ɍ��킹�ĉ摜��ύX
            HPImage.sprite = hpSprite[currentHP];
        }


    }
       
}
