using hikido;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace hikido
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameManagerSO gameManagerSO;
        [SerializeField] private SceneChanger sceneChanger;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HPManager hpManager;
        [SerializeField] private RankingManager _rankingManager;


        [Header("�X�R�A")]
        //TODO: �ԈႢ���ƂɃX�R�A��ς���悤�ɂ���
        [SerializeField] private int upScore = 0;
        [SerializeField] private int timeUpScore = 500;

        //���v�X�R�A
        public static int totalScore = 0;


        //�������ԗp�̃^�C��
        public static float aliveTime = 0;



        [Header("�X�R�A�p�t���O")]
        private bool endFlg = false;

        /*��������ǉ����܂����iby��ځj*/
        [Header("�X�R�A�pUI")]
        [SerializeField]
        private Text ScoreText;

        private static int  normalAndroidNumber;

        private static int  badAndroidNumber;

        public static int   NormalAndroidNumber => normalAndroidNumber;

        public static void AddNormalAndroidNumber() {  normalAndroidNumber++; }

        public static int   BadAndroidNumber => badAndroidNumber;
        public static void AddBadAndroidNumber() { badAndroidNumber++; }

        public static int TotalScore => totalScore;

        public static float AliveTime => aliveTime;

        private void Awake()
        {
            soundManager = FindObjectOfType<SoundManager>();
            sceneChanger = FindObjectOfType<SceneChanger>();
            hpManager = FindObjectOfType<HPManager>();
            _rankingManager = FindObjectOfType<RankingManager>();
        }

        //�X�^�[�g����Action�Ɋ֐���ݒ�
        private void Start()
        {
            //�Ó]����
            if (Fade.Instance != null)
            {

                StartCoroutine(Fade.Instance.FadeIn(0, 1f));
            }

            //��x�����ŏ��ɌĂяo��
            InvokeRepeating("TimeCountUP", 0.0f, 1.0f);
            IngameStart();

            totalScore = 0;
            aliveTime = 0;
        }

        private void Update()
        {
            CountAliveTime();
        }

        //�������Ԃ̃J�E���g������
        private void CountAliveTime()
        {
            aliveTime += Time.deltaTime;
        }

        private void OnEnable()
        {
            gameManagerSO.OnAddScore += ScoreUP;
            gameManagerSO.OutGame += EndGgme;
        }

        private void OnDisable()
        {
            gameManagerSO.OnAddScore -= ScoreUP;
            gameManagerSO.OutGame -= EndGgme;
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
                //soundManager.BGMStop();

                //����scene�ւ̑J��
                //TODO;���b�Ԃ������炷�悤�ɂ���
                //SceneManager.LoadScene("ResultScene");

                //score�̕ۑ�
                _rankingManager.SaveScore(totalScore);
                Debug.Log(totalScore + "���݂̃X�R�A ");

                //aliveTime�̕ۑ�
                _rankingManager.SaveAliveTime(aliveTime);
                Debug.Log(aliveTime + "���݂̃^�C�� ");

                //test
                //SceneManager.LoadScene("00_TitleScene_hikido");

            }
        }


        /// <summary> /// ���Ԍo�߂ɂ��X�R�A�̉��Z /// </summary>
        private void TimeCountUP()
        {
            //totalScore += timeUpScore;
            //uIlabel.ScoreUP.text = totalScore.ToString();
            //Debug.Log(totalScore);
        }

        /// <summary> /// �X�R�A�̉��Z /// </summary>
        private void ScoreUP()
        {
            //totalScore += upScore;
            //ingame���̂݃X�R�A�����Z
            //enum�ŃL�����N�^�[�^�C�v��ݒ肵�Ă���
            //�A���h���C�h���͂����ƃX�R�A���Z
            //������
            totalScore += upScore;
            /*
            else if(endFlg) 
            {
                //���Ԍo�߂ɂ��X�R�A���Z���~�߂�
                CancelInvoke();

            }
             */
            ScoreUI(totalScore);
        }

        private void ScoreUI(int score)
        {
            ScoreText.text = totalScore.ToString();
        }

        /// <summary> /// �X�R�A�������� /// </summary>
        public void ResetScore()
        {
            totalScore = 0;
        }
        
    }

}
