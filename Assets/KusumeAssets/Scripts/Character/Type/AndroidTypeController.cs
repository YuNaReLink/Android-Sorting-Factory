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
            //�^�C�v��I��
            type = info.type;
            //�摜��ύX
            spriteRenderer.sprite = info.images[Random.Range(0, info.images.Length)];
            //�G�t�F�N�g�t���O��ݒ�
            particleSystem.gameObject.SetActive(info.effectFlag);
        }
    }
}
