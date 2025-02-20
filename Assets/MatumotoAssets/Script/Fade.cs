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
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        fadeCanvas.enabled = false;
    }
    public IEnumerator FadeOut(int cl, float duration = 1f, Action onComplete = null)
    {
        float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        yield return StartCoroutine(Fading(cl, 0f, 1f, fadeDuration, onComplete));
        onComplete?.Invoke(); //�֐��ɉ��������Ă���Ǝ��s}
    }

    public IEnumerator FadeIn(int cl, float duration = 1f, Action onComplete = null)
    {
        float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        yield return StartCoroutine(Fading(cl, 1f, 0f, fadeDuration, onComplete));
        onComplete?.Invoke(); //�֐��ɉ��������Ă���Ǝ��s
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
        Debug.Log("YONDA?");
        fadeCanvas.enabled = true;
        float elapsedTime = 0;
        Color color = fadeImagies[cl].color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
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
