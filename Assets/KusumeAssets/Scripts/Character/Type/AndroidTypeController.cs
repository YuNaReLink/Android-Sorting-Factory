using UnityEngine;

namespace Kusume
{
    public enum AndroidType
    {
        Normal,
        Bad,
        VeryBad
    }

    public class AndroidTypeController : MonoBehaviour
    {
        [SerializeField]
        private AndroidType type;

        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private new ParticleSystem particleSystem;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeType(AndroidLedgerInfo info)
        {
            spriteRenderer.sprite = info.image;
            particleSystem.gameObject.SetActive(info.effectFlag);
        }
    }
}
