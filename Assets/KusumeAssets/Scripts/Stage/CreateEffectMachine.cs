using UnityEngine;

namespace Kusume
{
    public class CreateEffectMachine : MonoBehaviour
    {
        [SerializeField]
        private new ParticleSystem particleSystem;

        private Transform effects;


        private Impact impact;

        private void Awake()
        {
            impact = GetComponent<Impact>();
        }

        private void Start()
        {
            effects = new GameObject("EffectParent").transform;
            effects.SetParent(transform.parent);
        }

        /*
         * �A�C�e�������֐�
         * �������F�����ʒu
         * �������F�������̉�]
         */
        public ParticleSystem InitEffect(Vector3 pos, Quaternion rotation)
        {
            //�A�N�e�B�u�łȂ��I�u�W�F�N�g��bullets�̒�����T��
            foreach (Transform t in effects)
            {
                if (!t.gameObject.activeSelf)
                {
                    //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                    t.SetPositionAndRotation(pos, rotation);
                    //�A�N�e�B�u�ɂ���
                    t.gameObject.SetActive(true);
                    return t.GetComponent<ParticleSystem>();
                }
            }
            //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��ꍇ�V�K����

            //�������ɃA�C�e���̎q�I�u�W�F�N�g�ɂ���
            return Instantiate(particleSystem, pos, rotation, effects);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            AndroidTypeController controller = collision.gameObject.GetComponent<AndroidTypeController>();
            if (controller == null) { return; }
            if (!impact.Crush) { return; }
            InitEffect(controller.transform.position,particleSystem.transform.rotation);
        }
    }
}
