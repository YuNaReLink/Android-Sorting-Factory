using UnityEngine;

namespace Kusume
{
    /*
     * アンドロイドを指定した方向に飛ばすクラス
     */
    [RequireComponent(typeof(CreateEffectMachine))]
    public class Impact : MonoBehaviour
    {

        [SerializeField]
        private Vector3     impactDirOffset = new Vector3(-1,1,0);

        [SerializeField]
        private float       minImpactY = 0.25f;
        [SerializeField]
        private float       maxImpactY = 0.75f;

        [SerializeField]
        private float       power = 1f;

        // 回転の強さ
        [SerializeField]
        private float       torquePower = 1f;

        [SerializeField]
        private bool        crush = false;
        public bool         Crush => crush;

        [SerializeField]
        private Vector3     rayOffset;

        [SerializeField]
        private LayerMask layerMask;

        private void Start()
        {
            /*
            leftRange = transform.position.x - (transform.localScale.x * 0.5f);
            rightRange = transform.position.x + (transform.localScale.x * 0.5f);
             */
        }

        private void Update()
        {
            Ray2D ray = new Ray2D(transform.position + rayOffset, -Vector2.up); // Rayを生成、-transform.upは進行方向
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 4f,layerMask);//Raycastを生成

            Debug.DrawRay(ray.origin, ray.direction * 4f, Color.green, 0.015f); // 長さ1f、緑色で1フレーム可視化
            if (hit.collider)
            {
                AndroidTypeController controller = hit.collider.gameObject.GetComponent<AndroidTypeController>();
                if (controller == null) { return; }
                crush = true;
            }
            else
            {
                crush = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            AndroidTypeController controller = collision.gameObject.GetComponent<AndroidTypeController>();
            if (controller == null) { return; }
            RobotMove movement = controller.GetComponent<RobotMove>();
            movement.Stop();
            movement.SetStopFlag(true);


            if (!crush)
            {
                Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
                rigidbody2D.freezeRotation = false;
                // 力を加える
                Vector3 dir = new Vector3(impactDirOffset.x * 0.1f, Random.Range(minImpactY, maxImpactY), impactDirOffset.z);
                rigidbody2D.AddForce(dir * power, ForceMode2D.Impulse);
                // ランダムな回転を加える（正または負のトルク）
                float randomTorque = Random.Range(-torquePower, torquePower);
                rigidbody2D.AddTorque(randomTorque, ForceMode2D.Impulse);
                BoxCollider2D boxCollider = controller.GetComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }
            else
            {
                movement.Disable();
            }
        }
    }
}
