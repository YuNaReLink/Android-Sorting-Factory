using Kusume;
using System;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    public void SetMoveSpeed(float speed) {  moveSpeed = speed; }
    [SerializeField] Transform goalPoint;

    [SerializeField] AndroidTypeController _controller;

    // Start is called before the first frame update
    private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    public event Action<bool> OnFinishEvent;

    private bool stopFlag;

    bool dmgflg = true;

    public void SetStopFlag(bool s) { stopFlag = s; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        _controller = GetComponent<AndroidTypeController>();
        
    }
    private void Start()
    {
        goalPoint = LevelManager.main.goalPoint;
    }

    private void OnEnable()
    {
        stopFlag = false;
        boxCollider.isTrigger = false;
        rb.gravityScale = 2;
        rb.freezeRotation = true;
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
        dmgflg = true;
        rb.velocity = Vector2.zero;
    }
    /*
    public void Disable()
    {
        gameObject.SetActive(false);
        OnFinishEvent?.Invoke(false);

        //スコア追加
        _gameManagerSO.OnAddScore?.Invoke();
    }
    private void OnBecameInvisible()
    {
        Disable();
    }
     */
}
