using UnityEngine;

namespace Kusume
{
    [System.Serializable]
    public struct AndroidLedgerInfo
    {
        public AndroidType  type;
        public float        life;
        public Sprite       image;
        public Sprite[]     images;
        public bool         effectFlag;
    }
    [CreateAssetMenu(fileName = "AndroidLedger", menuName = "Ledger/AndroidLedger", order = 1)]
    public class AndroidLedger : ScriptableObject
    {
        [SerializeField]
        private AndroidLedgerInfo[] androidLedgerInfos;

        public AndroidLedgerInfo[] AndroidLedgerInfos => androidLedgerInfos;
    }
}
