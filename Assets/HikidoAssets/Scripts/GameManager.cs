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

        //TODO: �ԈႢ���ƂɃX�R�A��ς���悤�ɂ���
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;

        [Header("�X�R�A�p�t���O")]
        private bool endFlg = false;
        private bool successFlg = false;

        //�X�^�[�g����Action�Ɋ֐���ݒ�
        private void Start()
        {

        }

        private void IngameStart()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Action�Ɋ܂񂾊֐��̎g�p(�����̍Đ��A�V�[���؂�ւ��j

            }
        }



        /// <summary>�@/// �Q�[���I�����̏����@/// </summary>
        private void EndGgme()
        {
            //�����i�̗� = 0)
                //BGM�̒�~�Ɛ؂�ւ�

                //�X�R�A�̕ۑ�

                //����scene�ւ̑J��

        }

        /// <summary> /// �X�R�A�̉��Z /// </summary>
        private void ScoreUP()
        {
            if (gameManagerSO.Ingameflg)
            {
                while (!endFlg)
                {
                    totalScore += timeUpScore;
                }
                //TODO: �x���g�R���x�A�̏��������ō쐬���Ă���e�X�g
                //�A���h���C�h���͂����ƃX�R�A���Z
                //������
                totalScore += upScore;
            }
                
        }





    }

}
