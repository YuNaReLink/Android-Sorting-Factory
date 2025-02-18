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

        //��
        [SerializeField] private UIlabel uIlabel;


        [Header("�X�R�A")]
        //TODO: �ԈႢ���ƂɃX�R�A��ς���悤�ɂ���
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;
        public static int highScore = 0;
        public static int totalScore = 0;
        

        //���̃L�����N�^�[HP
        [SerializeField] private int CharactorHP = 0;


        [Header("�X�R�A�p�t���O")]
        private bool endFlg = false;
        private bool successFlg = false;


        //�X�^�[�g����Action�Ɋ֐���ݒ�
        private void Start()
        {
            //��x�����ŏ��ɌĂяo��
            InvokeRepeating("TimeCountUP", 0.0f, 1.0f);

        }

        private void Update()
        {
            //�I���t���O�����܂�
            if (!endFlg)
            {
                ScoreUP();
            }
        }

        /// <summary>�@/// ��Փx�I����@/// </summary>
        private void IngameStart()
        {

            //gamestart����Action���̊i�[���Ă���֐����g�p
            gameManagerSO.IngameStart?.Invoke();
        } 


        /// <summary>�@/// �Q�[���I�����̏����@/// </summary>
        private void EndGgme()
        {
            //�����i�̗� = 0) player����HP���Q��
            if(hpManager.EndFlg())
            {
                endFlg = true;
                gameManagerSO.OutGameflg = true;

                //BGM�̒�~�Ɛ؂�ւ�
                soundManager.BGMStop();

                //����scene�ւ̑J��
                //SceneManager.LoadScene("ResultScene");

        
            }



        }


        /// <summary> /// ���Ԍo�߂ɂ��X�R�A�̉��Z /// </summary>
        private void TimeCountUP()
        {
            totalScore += timeUpScore;
            uIlabel.ScoreUP.text = totalScore.ToString();
            Debug.Log(totalScore);
        }

        /// <summary> /// �X�R�A�̉��Z /// </summary>
        private void ScoreUP()
        {
            //ingame���̂݃X�R�A�����Z
            if (gameManagerSO.Ingameflg)
            {
                //TODO: �x���g�R���x�A�̏��������ō쐬���Ă���e�X�g
                //�A���h���C�h���͂����ƃX�R�A���Z
                //������
                    //totalScore += upScore;
            }
            else if(endFlg) 
            {
                //���Ԍo�߂ɂ��X�R�A���Z���~�߂�
                CancelInvoke();
                highScore = totalScore;
                if(highScore < totalScore)
                {
                    totalScore = highScore;
                }
            }

        }

        /// <summary> /// �X�R�A�������� /// </summary>
        public void ResetScore()
        {
            totalScore = 0;
        }

       

        
    }

}
