using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kusume
{
    /*
     * アンドロイドを指定した方向に飛ばすクラス
     */
    [RequireComponent(typeof(CreateEffectMachine))]
    public class Impact : MonoBehaviour
    {

        [SerializeField]
        private Vector3     impactDirOffset = new Vector3(-1,1,0);

        [SerializeField]
        private float       minImpactY = 0.25f;
        [SerializeField]
        private float       maxImpactY = 0.75f;

        [SerializeField]
        private float       power = 1f;

        // 回転の強さ
        [SerializeField]
        private float       torquePower = 1f;

        [SerializeField]
        private bool        crush = false;
        public bool         Crush => crush;
        [SerializeField]
        private bool        longCrush = false;
        public bool         longLongCrush => longCrush;

        [SerializeField]
        private Vector3     rayOffset;
        [SerializeField]
        private Vector3[]   rayOffsets;

        [SerializeField]
        private LayerMask layerMask;

        //public float StopTime;

        public event Action<Impact> OnHit;
        public event Action<Impact> OnImpact;

        private CreateEffectMachine effectMachine;

        private AndroidTypeController controller;

        private List<AndroidTypeController> controllers = new List<AndroidTypeController>();

        private void Awake()
        {
            effectMachine = GetComponent<CreateEffectMachine>();
        }

        private void Update()
        {
            Ray2D[] rays = new Ray2D[3];
            RaycastHit2D[] hits = new RaycastHit2D[3];

            for(int i = 0; i < rays.Length; i++)
            {
                rays[i] = new Ray2D(transform.position + rayOffsets[i], -Vector2.up); // Rayを生成、-transform.upは進行方向
                hits[i] = Physics2D.Raycast(rays[i].origin, rays[i].direction, 4f, layerMask);//Raycastを生成
                Debug.DrawRay(rays[i].origin, rays[i].direction * 4f, Color.green, 0.015f); // 長さ1f、緑色で1フレーム可視化
            }

            if (hits[0].collider|| hits[1].collider || hits[2].collider)
            {
                crush = true;
                for(int i = 0; i < hits.Length; i++)
                {
                    AndroidTypeController controller = hits[i].collider?.gameObject.GetComponent<AndroidTypeController>();
                }
                if (controller == null) { return; }
                RobotMove movement = controller.GetComponent<RobotMove>();
            }
            else
            {
                crush = false;
            }
        }

        private void AddImpact(AndroidTypeController controller)
        {
            DisableCheck disableCheck = controller.GetComponent<DisableCheck>();
            if (!crush)
            {
                RobotMove movement = controller.GetComponent<RobotMove>();
                movement.Stop();
                movement.SetStopFlag(true);

                Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
                rigidbody2D.freezeRotation = false;
                // 力を加える
                Vector3 dir = new Vector3(impactDirOffset.x * 0.1f, UnityEngine.Random.Range(minImpactY, maxImpactY), impactDirOffset.z);
                rigidbody2D.AddForce(dir * power, ForceMode2D.Impulse);
                // ランダムな回転を加える（正または負のトルク）
                float randomTorque = UnityEngine.Random.Range(-torquePower, torquePower);
                rigidbody2D.AddTorque(randomTorque, ForceMode2D.Impulse);
                BoxCollider2D boxCollider = controller.GetComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
                
                disableCheck.BadDamageCheck(controller);
            }
            else
            {
                Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0;
                
                BoxCollider2D boxCollider = controller.GetComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;

                disableCheck.NormalDamageCheck(controller);
                controller.ChangeScrap();
            }
        }

        public void LongCrush()
        {
            crush = true;
            for(int i = 0; i < controllers.Count; i++)
            {
                AddImpact(controllers[i]);
                effectMachine.CreateEffect(controllers[i]);
                DisableCheck disableCheck = controllers[i].GetComponent<DisableCheck>();
                disableCheck.Disable();
                disableCheck.NormalDamageCheck(controllers[i]);
                controllers.RemoveAt(i);
            }
        }

        void AddUnique(AndroidTypeController number)
        {
            if (!controllers.Contains(number))
            {
                controllers.Add(number);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            controller = collision.gameObject.GetComponent<AndroidTypeController>();
            if (controller == null) { return; }
            if (controller.LifeCheck() && crush)
            {
                AddUnique(controller);
                Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0;

                RobotMove movement = controller.GetComponent<RobotMove>();
                movement.Stop();
                movement.SetStopFlag(true);

                BeltConveyorController.Instance.AllMoveActivate(false);
                BoxCollider2D boxCollider = controller.GetComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }
            else
            {
                AddImpact(controller);
                effectMachine.CreateEffect(controller);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            controller = collision.gameObject.GetComponent<AndroidTypeController>();
            if (controller == null) { return; }
            if (controller.LifeCheck())
            {
                Rigidbody2D rigidbody2D = controller.GetComponent<Rigidbody2D>();
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                RobotMove movement = controller.GetComponent<RobotMove>();
                movement.Stop();
                movement.SetStopFlag(false);
            }
        }
    }
}
