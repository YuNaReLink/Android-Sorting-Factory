using UnityEngine;

namespace Kusume
{
    /*
     * �A���h���C�h���w�肵�������ɔ�΂��N���X
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

        // ��]�̋���
        [SerializeField]
        private float       torquePower = 1f;

        [SerializeField]
        private bool        crush = false;
        public bool         Crush => crush;

        [SerializeField]
        private Vector3     rayOffset;

        [SerializeField]
        private LayerMask layerMask;

        private void Update()
        {
            Ray2D ray = new Ray2D(transform.position + rayOffset, -Vector2.up); // Ray�𐶐��A-transform.up�͐i�s����
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 4f,layerMask);//Raycast�𐶐�

            Debug.DrawRay(ray.origin, ray.direction * 4f, Color.green, 0.015f); // ����1f�A�ΐF��1�t���[������
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
                // �͂�������
                Vector3 dir = new Vector3(impactDirOffset.x * 0.1f, Random.Range(minImpactY, maxImpactY), impactDirOffset.z);
                rigidbody2D.AddForce(dir * power, ForceMode2D.Impulse);
                // �����_���ȉ�]��������i���܂��͕��̃g���N�j
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
