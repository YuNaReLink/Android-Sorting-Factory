using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    public class ExitExplanation : MonoBehaviour
    {
        private RectTransform rectTransform;

        [SerializeField]
        private float speed = 5f;

        private bool scroll = false;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Time.timeScale = 0;
            scroll = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                scroll = true;
            }
            if (!scroll) { return; }

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition,new Vector2(0,1080), speed);

            Vector2 sub = rectTransform.anchoredPosition - new Vector2(0, 1080);
            if (sub.magnitude < 0.15f)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
    }
}
