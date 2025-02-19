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
         * アイテム生成関数
         * 第一引数：生成位置
         * 第二引数：生成時の回転
         */
        public ParticleSystem InitEffect(Vector3 pos, Quaternion rotation)
        {
            //アクティブでないオブジェクトをbulletsの中から探索
            foreach (Transform t in effects)
            {
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, rotation);
                    //アクティブにする
                    t.gameObject.SetActive(true);
                    return t.GetComponent<ParticleSystem>();
                }
            }
            //非アクティブなオブジェクトがない場合新規生成

            //生成時にアイテムの子オブジェクトにする
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
