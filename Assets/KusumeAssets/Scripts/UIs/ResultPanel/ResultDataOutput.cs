using hikido;
using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    public class ResultDataOutput : MonoBehaviour
    {
        [SerializeField]
        private Text scoreUI;

        [SerializeField]
        private Text normalAndroidNumber;
        [SerializeField]
        private Text badAndroidNumber;

        public void Output()
        {
             if(GameManager.ResultScore > 99999999)
            {
                scoreUI.text = "99999999+";
            }
            else
            {
                scoreUI.text = GameManager.ResultScore.ToString();
            }

            if(GameManager.NormalAndroidNumber > 999)
            {
                normalAndroidNumber.text = "999+";
            }
            else
            {
                normalAndroidNumber.text = GameManager.NormalAndroidNumber.ToString();
            }

            if (GameManager.BadAndroidNumber > 999)
            {
                badAndroidNumber.text = "999+";
            }
            else
            {
                badAndroidNumber.text = GameManager.BadAndroidNumber.ToString();
            }
        }
    }
}
