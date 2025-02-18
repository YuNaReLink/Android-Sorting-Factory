using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    /*
     * �{�^����UI��ύX����N���X
     * ���̃N���X���A�^�b�`���邾���Ń{�^����UI��ύX�ł���
     */
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class ButtonUI : MonoBehaviour
    {

        [SerializeField]
        private Sprite      normalImage;

        [SerializeField]
        private Sprite      pressedImage;

        private Button      button;

        private Image       buttonImage;

        private Action      onPressed;

        private Action      onRelease;

        private void Awake()
        {
            button = GetComponent<Button>();
            buttonImage = GetComponent<Image>();
            buttonImage.sprite = normalImage;

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
            InputChangeUI();
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
            buttonImage.sprite = pressedImage;
        }

        private void OnRelease()
        {
            buttonImage.sprite = normalImage;
        }
    }
}
