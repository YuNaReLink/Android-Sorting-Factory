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
        private new UITagList tag;
        public UITagList Tag => tag;
    }
}
