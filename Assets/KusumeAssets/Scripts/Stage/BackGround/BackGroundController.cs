using UnityEngine;

namespace Kusume
{
    /*
     * ゲーム開始時に背景を変更する
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
