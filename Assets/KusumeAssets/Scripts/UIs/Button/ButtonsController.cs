using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    /*
     * 複数あるボタンをSPACEで変更して決定を行うクラス
     */
    public class ButtonsController : MonoBehaviour
    {
        private Button[]    buttons;

        private int         buttonIndex;

        [SerializeField]
        private Image       selectImage;

        [SerializeField]
        private Vector2     selectImageOffset;

        private Timer       decideTimer = new Timer();
        [SerializeField]
        private float       count = 0.2f;

        private void Awake()
        {
            buttons = GetComponentsInChildren<Button>();
        }

        private void OnEnable()
        {
            decideTimer.OnEnd += ChangeButton;
        }

        private void OnDisable()
        {
            decideTimer.OnEnd -= ChangeButton;
        }

        private void Update()
        {
            decideTimer.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                decideTimer.Start(count);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                DecideCheck();
            }
        }

        private void DecideCheck()
        {
            if (decideTimer.IsEnd())
            {
                buttons[buttonIndex].onClick?.Invoke();
            }
        }

        private void ChangeButton()
        {
            if (Input.GetKey(KeyCode.Space)) { return; }
            buttonIndex++;
            if(buttonIndex > buttons.Length - 1)
            {
                buttonIndex = 0;
            }
            selectImage.rectTransform.anchoredPosition = buttons[buttonIndex].GetComponent<RectTransform>().anchoredPosition;
            selectImage.rectTransform.anchoredPosition += selectImageOffset;
        }
    }
}
