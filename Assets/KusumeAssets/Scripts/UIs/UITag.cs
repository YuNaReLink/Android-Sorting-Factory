using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    public enum UITagList
    {
        ResultScore,
        ResultNormalAndroidNumber,
        ResultBadAndroidNumber
    }
    public class UITag : MonoBehaviour
    {
        [SerializeField]
        private UITagList tag;
        public UITagList Tag => tag;
    }
}
