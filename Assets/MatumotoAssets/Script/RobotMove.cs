using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform goalPoint;
    // Start is called before the first frame update
   private Rigidbody2D rb;

    public event Action<bool> OnFinishEvent; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startPoint = LevelManager.main.startPoint;
        goalPoint = LevelManager.main.goalPoint;
    }
    private void Update()
    {
        if (Vector2.Distance(goalPoint.position, transform.position) <= 0.1f)
        {
            gameObject.SetActive(false); 
            OnFinishEvent?.Invoke(false);
            transform.position = startPoint.position;
        }
        
    }
    private void FixedUpdate()
    {
       //向きを決めるためのtarget.positionは上のアップデートでやってるよ
       //向きを決める...座標ー座標で。それをノーマライズすることで長さを1にする
        Vector2 direction = (goalPoint.position - transform.position).normalized;
        //向き*速度で毎フレーム動かす
        rb.velocity = direction * moveSpeed;
    }
   
}
