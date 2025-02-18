using UnityEngine;

namespace Kusume
{
    public class Movement : MonoBehaviour
    {
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var moveVelocity = rigidbody2D.velocity;
            moveVelocity.x = speed;
            rigidbody2D.velocity = new Vector3(moveVelocity.x, rigidbody2D.velocity.y);
        }
    }
}

