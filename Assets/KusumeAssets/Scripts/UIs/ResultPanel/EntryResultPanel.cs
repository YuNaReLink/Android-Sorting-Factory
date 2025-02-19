using UnityEngine;

namespace Kusume
{
    public class EntryResultPanel : MonoBehaviour
    {
        private RectTransform rectTransform;

        [SerializeField]
        private float entrySpeed = 1.0f;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        // Start is called before the first frame update
        private void Start()
        {
            rectTransform.localScale = Vector3.zero;
        }

        private void Update()
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale,Vector3.one,entrySpeed * Time.deltaTime);
            if(rectTransform.localScale.x >= 1f)
            {
                rectTransform.localScale = Vector3.one;
                enabled = false;
            }
        }
    }
}
