using UnityEngine;

namespace Kusume
{
    public class Movement : MonoBehaviour
    {
        private new Rigidbody2D     rigidbody2D;

        [SerializeField]
        private float               speed;

        private bool                stopFlag;

        public void SetSpeed(float s) { speed = s; }

        public void SetStopFlag(bool s) {  stopFlag = s; }

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            stopFlag = false;
            rigidbody2D.freezeRotation = true;
        }

        private void Update()
        {
            if (stopFlag) { return; }
            Move();
        }

        private void Move()
        {
            var moveVelocity = rigidbody2D.velocity;
            moveVelocity.x = speed;
            rigidbody2D.velocity = new Vector3(moveVelocity.x, rigidbody2D.velocity.y);
        }

        public void Stop()
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}

