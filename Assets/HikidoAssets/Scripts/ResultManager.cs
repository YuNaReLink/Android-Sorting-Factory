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

        [Header("�e�X�g�p")]
        int genuineProduct = 50;
        int defectiveProduct = 3;

        private void Start()
        {
            //Action�Ɋi�[ -> result��ʗp��BGM�Đ�
            gameManagerSO.OutGame?.Invoke();
            Result();
        }


        private void Result()
        {
            StartCoroutine(ResultDisplay());
        }

        //�X�R�A�\��(���j
        private IEnumerator ResultDisplay()
        {
            yield return oneSec;

            uiLabel.ScoreUP.text = GameManager.totalScore.ToString();

            yield return oneSec ;

            uiLabel.GenuineProduct.text = genuineProduct.ToString();

            yield return oneSec;

            uiLabel.defectiveProduct.text = defectiveProduct.ToString();

        }

        //TODO:���U���g��ʂ���^�C�g���ɖ߂�Ƃ���BGMstop�̏���

        
    }

}
