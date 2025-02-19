using Kusume;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform goalPoint;
    [SerializeField] private GameManagerSO _gameManagerSO;

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
        rb.freezeRotation = true;
    }
    private void Update()
    {
        float dis = goalPoint.position.x - transform.position.x;
        if (MathF.Abs(dis) <= 1f)
        {
            
            gameObject.SetActive(false); 
            OnFinishEvent?.Invoke(false);

            //�^�C�v�Ŏ󂯂�_���[�W��ύX
            switch (_controller.Type)
            {
                case AndroidType.Normal:
                    if (dmgflg) { _gameManagerSO.MistakeDamage?.Invoke(); }
                    break;
                case AndroidType.Bad:
                    _gameManagerSO.OnAddDamage?.Invoke(1);
                    break;
                case AndroidType.VeryBad:
                    _gameManagerSO.OnAddDamage?.Invoke(3);
                    break;         
            }
            
        }
        
    }
    private void FixedUpdate()
    {
        if (stopFlag){return;}
        //���������߂邽�߂�target.position�͏�̃A�b�v�f�[�g�ł���Ă��
        //���������߂�...���W�[���W�ŁB������m�[�}���C�Y���邱�ƂŒ�����1�ɂ���
        Vector2 direction = (goalPoint.position - transform.position).normalized;
        direction.y = rb.velocity.y;
        //����*���x�Ŗ��t���[��������
        rb.velocity = direction * moveSpeed;
    }

    public void Stop()
    {
        dmgflg = true;
        rb.velocity = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        OnFinishEvent?.Invoke(false);

        //�X�R�A�ǉ�
        _gameManagerSO.OnAddScore?.Invoke();
    }
}
