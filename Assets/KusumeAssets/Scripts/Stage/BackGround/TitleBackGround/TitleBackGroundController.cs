using Kusume;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    public class TitleBackGroundController : MonoBehaviour
    {
        [SerializeField]
        private Sprite[]        backs;

        [SerializeField]
        private Sprite          specialBack;

        private SpriteRenderer  spriteRenderer;

        private float thankForPlayingBackRatio = 0.1f;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            float randomRatio = Random.value;
            if(randomRatio < thankForPlayingBackRatio)
            {
                spriteRenderer.sprite = specialBack;
            }
            else
            {
                spriteRenderer.sprite = backs[(int)GameLevelManager.GameLevel];
            }
        }
    }
}
