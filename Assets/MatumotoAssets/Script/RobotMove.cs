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
       //���������߂邽�߂�target.position�͏�̃A�b�v�f�[�g�ł���Ă��
       //���������߂�...���W�[���W�ŁB������m�[�}���C�Y���邱�ƂŒ�����1�ɂ���
        Vector2 direction = (goalPoint.position - transform.position).normalized;
        //����*���x�Ŗ��t���[��������
        rb.velocity = direction * moveSpeed;
    }
   
}
