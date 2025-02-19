using UnityEngine;

namespace Kusume
{
    /*
     * ƒQ[ƒ€ŠJn‚É”wŒi‚ğ•ÏX‚·‚é
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
