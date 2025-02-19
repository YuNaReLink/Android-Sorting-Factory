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
        private  AndroidType         type;

        public AndroidType Type => type;

        private SpriteRenderer      spriteRenderer;
        [SerializeField]
        private new ParticleSystem  particleSystem;

        [SerializeField]
        private float               life;

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
            if (info.effectFlag)
            {
                RandomSetEffect(info);
            }
            else
            {
                particleSystem.gameObject.SetActive(false);
            }
            life = info.life;
        }

        private void RandomSetEffect(AndroidLedgerInfo info)
        {
            if(Random.value > 0.5)
            {
                particleSystem.gameObject.SetActive(true);
            }
            else
            {
                particleSystem.gameObject.SetActive(false);
            }
        }

        public bool AndroidLife()
        {
            life -= Time.deltaTime;
            return life <= 0;
        }
    }
}
