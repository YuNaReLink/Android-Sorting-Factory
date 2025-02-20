using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Fade : MonoBehaviour
{
    public static Fade Instance { get; private set; }

    [SerializeField] private Canvas fadeCanvas;

    [SerializeField] private float defaultFadeDuration = 1f;

    //Fade�̃J���[�o���G�[�V�������Q��ޗp�ӂ��܂����@cl�̉�
    [SerializeField] private Image[] fadeImagies;

    private Color[] colors = new Color[]
    {
        Color.black,
        Color.red
    };
    private void Awake()
    {
        Instance = this;
        /*
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
         */

        fadeCanvas.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(FadeIn(0, 1f));
    }
    public IEnumerator FadeOut(int cl, float duration = 1f, Action onComplete = null)
    {
        float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        yield return StartCoroutine(Fading(cl, 0f, 1f, fadeDuration, onComplete));
        onComplete?.Invoke(); //�֐��ɉ��������Ă���Ǝ��s}
        onComplete = null;
    }

    public IEnumerator FadeIn(int cl, float duration = 1f, Action onComplete = null)
    {
        float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        yield return StartCoroutine(Fading(cl, 1f, 0f, fadeDuration, onComplete));
        onComplete?.Invoke(); //�֐��ɉ��������Ă���Ǝ��s
        onComplete = null;
    }
    public void Equip(int cl, float duration = 1f, Action onComplete = null)
    {
        // float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        //StartCoroutine(GetEquip(cl, onComplete));
    }
    /// <summary>
    /// ����0 = ���@1���I�����W
    /// </summary>
    /// <param name="cl">�F�̎w��...�C���[�W�^�̔z��ɐF�X�ȃJ���[�̉摜����ꂽ�B����̎�ނ̎w��p</param>
    /// <param name="StartAlpha"></param>
    /// <param name="EndAlpha"></param>
    /// <param name="duration"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    private IEnumerator Fading(int cl, float StartAlpha, float EndAlpha, float duration, Action onComplete)
    {
        fadeCanvas.enabled = true;
        float elapsedTime = 0;
        Color color = colors[cl];

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(StartAlpha, EndAlpha, elapsedTime / duration);
            fadeImagies[cl].color = color;
            yield return null;
        }

        color.a = EndAlpha;
        fadeImagies[cl].color = color;
        if (EndAlpha == 0f) //�s�v�ɂȂ�����enabled���I�t�ɂ���H
        {
            fadeCanvas.enabled = false;
        }
    }

}
