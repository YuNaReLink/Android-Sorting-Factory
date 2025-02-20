using hikido;
using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal;
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

        //引土追加した箇所
        [SerializeField]
        public static SoundManager soundManager;


        [SerializeField]
        private Vector2     selectImageOffset;

        private Timer       decideTimer = new Timer();

        private float       count = 0.5f;

        private void Awake()
        {
            buttons = GetComponentsInChildren<Button>();
        }

        private IEnumerator InputActivate()
        {
            InputManager.SetInputFlag(false);
            yield return new WaitForSecondsRealtime(0.5f);
            InputManager.SetInputFlag(true);
        }

        private void OnEnable()
        {
            StartCoroutine(InputActivate());
            decideTimer.OnEnd += ChangeButton;
        }

        private void OnDisable()
        {
            decideTimer.OnEnd -= ChangeButton;
        }

        private void Update()
        {
            if (!InputManager.InputFlag) { return; }
            decideTimer.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                decideTimer.Start(count);
                SoundManager.SelectCheckSE();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                DecideCheck();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                decideTimer.End();
            }
        }

        private void DecideCheck()
        {
            if (decideTimer.IsEnd())
            {
                buttons[buttonIndex].onClick?.Invoke();
                InputManager.SetInputFlag(false);
            }
        }

        private void ChangeButton()
        {
            if (Input.GetKey(KeyCode.Space)) { return; }
            if (!InputManager.InputFlag) { return; }
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
