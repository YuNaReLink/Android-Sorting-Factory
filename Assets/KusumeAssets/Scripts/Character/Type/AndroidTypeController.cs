using UnityEngine;

namespace Kusume
{
    public enum AndroidType
    {
        Normal,
        Bad,
        VeryBad,
        Scrap
    }
    [RequireComponent(typeof(DisableCheck))]
    public class AndroidTypeController : MonoBehaviour
    {
        [SerializeField]
        private  AndroidType         type;

        [SerializeField]
        private new ParticleSystem  particleSystem;

        [SerializeField]
        private float               life;

        private DisableCheck        disableCheck;

        public AndroidType          Type => type;

        private SpriteRenderer      spriteRenderer;

        private Sprite              scrapSprite;

        private new Rigidbody2D     rigidbody2D;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();

            disableCheck = GetComponent<DisableCheck>();
        }

        private void Update()
        {
            disableCheck.OutScreenCheck(this);

            if(rigidbody2D.gravityScale == 0&&type == AndroidType.VeryBad)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    DecreaseLife();
                }
                else
                {
                    rigidbody2D.gravityScale = 2;

                    RobotMove movement = GetComponent<RobotMove>();
                    movement.SetStopFlag(false);

                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    boxCollider.isTrigger = false;
                }
            }
        }

        public void ChangeType(AndroidLedgerInfo info)
        {
            //タイプを選択
            type = info.type;
            int index = Random.Range(0, info.images.Length);
            //画像を変更
            spriteRenderer.sprite = info.images[index];
            scrapSprite = info.scrapImage[index];
            //エフェクトフラグを設定
            if (info.effectFlag)
            {
                RandomSetEffect(info);
            }
            else
            {
                particleSystem.gameObject.SetActive(false);
            }
            life = info.life;
        }

        public void ChangeScrap()
        {
            spriteRenderer.sprite = scrapSprite;
            type = AndroidType.Scrap;
            transform.localScale = Vector3.one;
        }

        private void RandomSetEffect(AndroidLedgerInfo info)
        {
            if(Random.value > 0.5)
            {
                particleSystem.gameObject.SetActive(true);
            }
            else
            {
                particleSystem.gameObject.SetActive(false);
            }
        }

        private void DecreaseLife()
        {
            life -= Time.deltaTime;
            if(life < 0)
            {
                rigidbody2D.gravityScale = 2;
                //BeltConveyorController.Instance.AllMoveActivate(true);
                BeltConveyorController.Instance.LongCrush();
            }
        }

        public bool LifeCheck()
        {
            return life > 0;
        }
    }
}
