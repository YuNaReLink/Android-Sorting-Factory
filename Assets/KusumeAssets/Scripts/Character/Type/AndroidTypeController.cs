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
        private AndroidType         type;

        private SpriteRenderer      spriteRenderer;
        [SerializeField]
        private new ParticleSystem  particleSystem;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeType(AndroidLedgerInfo info)
        {
            //タイプを選択
            type = info.type;
            //画像を変更
            spriteRenderer.sprite = info.images[Random.Range(0, info.images.Length)];
            //エフェクトフラグを設定
            particleSystem.gameObject.SetActive(info.effectFlag);
        }
    }
}
