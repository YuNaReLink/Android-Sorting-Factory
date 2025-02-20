using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    public enum ChargeUI
    {
        Frame,
        Gage
    }
    public class ChargeCrushUI : MonoBehaviour
    {
        [SerializeField]
        private Image[] chargeImage;

        private void Start()
        {
            Activate(false);
        }

        public void ChargeRatio(float max,float current)
        {
            chargeImage[(int)ChargeUI.Gage].fillAmount = current % max;
        }

        public void Activate(bool flag)
        {
            for(int i = 0; i < chargeImage.Length; i++)
            {
                chargeImage[i].enabled = flag;
            }
        }
    }
}
