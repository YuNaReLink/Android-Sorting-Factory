using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform goalPoint;
    // Start is called before the first frame update
   private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    public event Action<bool> OnFinishEvent;

    private bool stopFlag;

    public void SetStopFlag(bool s) { stopFlag = s; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        goalPoint = LevelManager.main.goalPoint;
    }

    private void OnEnable()
    {
        stopFlag = false;
        boxCollider.isTrigger = false;
        rb.freezeRotation = true;
    }
    private void Update()
    {
        float dis = goalPoint.position.x - transform.position.x;
        if (MathF.Abs(dis) <= 1f)
        {
            gameObject.SetActive(false); 
            OnFinishEvent?.Invoke(false);
        }
        
    }
    private void FixedUpdate()
    {
        if (stopFlag){return;}
        //向きを決めるためのtarget.positionは上のアップデートでやってるよ
        //向きを決める...座標ー座標で。それをノーマライズすることで長さを1にする
        Vector2 direction = (goalPoint.position - transform.position).normalized;
        direction.y = rb.velocity.y;
        //向き*速度で毎フレーム動かす
        rb.velocity = direction * moveSpeed;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        OnFinishEvent?.Invoke(false);
    }

    private void OnBecameInvisible()
    {
        Disable();
    }
}
