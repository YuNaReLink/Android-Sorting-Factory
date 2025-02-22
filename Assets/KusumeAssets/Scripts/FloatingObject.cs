using UnityEngine;

namespace Kusume
{
    public class FloatingObject : MonoBehaviour
    {
        // 移動速度
        [SerializeField]
        private float speed = 2f; 
        // 移動範囲
        [SerializeField]
        private float height = 2f; 

        private float startY;

        private void Start()
        {
            startY = transform.position.y;
        }

        private void Update()
        {
            float newY = startY + Mathf.PingPong(Time.time * speed, height * 2) - height;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
