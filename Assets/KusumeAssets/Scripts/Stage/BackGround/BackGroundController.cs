using UnityEngine;

namespace Kusume
{
    /*
     * �Q�[���J�n���ɔw�i��ύX����
     */
    public class BackGroundController : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] backs;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            spriteRenderer.sprite = backs[(int)GameLevelManager.GameLevel];
        }
    }
}
