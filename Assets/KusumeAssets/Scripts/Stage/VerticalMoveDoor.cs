using System;
using UnityEngine;

namespace Kusume
{
    public class VerticalMoveDoor : MonoBehaviour
    {
        [Header("�Q�[�����s���Ɉʒu��ݒ肷�邩����")]
        [SerializeField]
        private bool        startSettingFlag = false;
        [Header("�I�u�W�F�N�g�̌��ʒu")]
        [SerializeField]
        private Vector3     basePosition;
        [Header("�I�u�W�F�N�g�ړ���̈ʒuY")]
        [SerializeField]
        private float       verticalOffsetY;
        [Header("�ړ��X�s�[�h")]
        [SerializeField]
        private float       speed;

        private Vector3     MovePosition => new Vector3(basePosition.x, basePosition.y + verticalOffsetY, basePosition.z);

        private bool        close;

        private Action      onPressed;

        private Action      onRelease;


        private void Start()
        {
            if (startSettingFlag)
            {
                basePosition = transform.position;
            }
            else
            {
                transform.position = basePosition;
            }
        }

        private void OnEnable()
        {
            onPressed += OnPressed;
            onRelease += OnRelease;
        }

        private void OnDisable()
        {
            onPressed -= OnPressed;
            onRelease -= OnRelease;
        }

        private void Update()
        {
            MoveDoor();

            InputChangeUI();
        }

        private void MoveDoor()
        {
            if (!close)
            {
                transform.position = Vector3.Lerp(transform.position, basePosition, speed * Time.deltaTime);
                if ((transform.position - basePosition).magnitude <= 0.01f)
                {
                    transform.position = basePosition;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, MovePosition,speed * Time.deltaTime);
                if((transform.position - MovePosition).magnitude <= 0.01f)
                {
                    transform.position = MovePosition;
                }
            }
        }

        private void InputChangeUI()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onPressed?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                onRelease?.Invoke();
            }
        }
        private void OnPressed()
        {
            close = true;
        }

        private void OnRelease()
        {
            close = false;
        }
    }
}
