using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    public enum LifeType
    {
        Null,
        Min,
        Half,
        Max
    }

    public class LifeController : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] lifes;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            ChangeLifeUI((int)LifeType.Max);
        }

        public void ChangeLifeUI(int count)
        {
            image.sprite = lifes[count];
        }
    }
}
