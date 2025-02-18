using UnityEngine;

namespace Kusume
{
    public class UIEntry : MonoBehaviour
    {
        [SerializeField]
        private bool scaleX;
        [SerializeField]
        private bool scaleY;

        [SerializeField]
        private float speed;

        private void Start()
        {
            Vector3 s = Vector3.one;
            if (scaleX)
            {
                s.x = 0f;
            }
            if (scaleY)
            {
                s.y = 0f;
            }
            Vector3 scale = transform.localScale;
            scale = s;
            transform.localScale = scale;
        }

        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one,speed * Time.deltaTime);
            Vector3 sub = transform.localScale - Vector3.one;
            if(sub.magnitude <= 0.01f)
            {
                transform.localScale = Vector3.one;
                Destroy(this);
            }
        }
    }
}
