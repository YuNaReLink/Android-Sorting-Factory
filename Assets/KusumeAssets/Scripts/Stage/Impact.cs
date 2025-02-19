using UnityEngine;

namespace Kusume
{
    public class Impact : MonoBehaviour
    {
        [SerializeField]
        private Vector3 impactDirOffset = new Vector3(-1,1,0);

        [SerializeField]
        private float power = 1f;

        // ��]�̋���
        [SerializeField]
        private float torquePower = 1f; 

        private void OnCollisionEnter2D(Collision2D collision)
        {
            AndroidTypeController controller = collision.gameObject.GetComponent<AndroidTypeController>();
            if (controller == null) { return; }
            RobotMove movement = controller.GetComponent<RobotMove>();
            movement.Stop();
            movement.SetStopFlag(true);

            Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
            rigidbody2D.freezeRotation = false;

            // �͂�������
            rigidbody2D.AddForce(impactDirOffset * power, ForceMode2D.Impulse);

            // �����_���ȉ�]��������i���܂��͕��̃g���N�j
            float randomTorque = Random.Range(-torquePower, torquePower);
            rigidbody2D.AddTorque(randomTorque, ForceMode2D.Impulse);

            BoxCollider2D boxCollider = controller.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }
    }
}
