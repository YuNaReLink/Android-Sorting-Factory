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

    //Fadeのカラーバリエーションを２種類用意しました　clの価
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
        onComplete?.Invoke(); //関数に価が入っていると実行}
        onComplete = null;
    }

    public IEnumerator FadeIn(int cl, float duration = 1f, Action onComplete = null)
    {
        float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        yield return StartCoroutine(Fading(cl, 1f, 0f, fadeDuration, onComplete));
        onComplete?.Invoke(); //関数に価が入っていると実行
        onComplete = null;
    }
    public void Equip(int cl, float duration = 1f, Action onComplete = null)
    {
        // float fadeDuration = duration > 0f ? duration : defaultFadeDuration;
        //StartCoroutine(GetEquip(cl, onComplete));
    }
    /// <summary>
    /// 現状0 = 黒　1＝オレンジ
    /// </summary>
    /// <param name="cl">色の指定...イメージ型の配列に色々なカラーの画像を入れた。それの種類の指定用</param>
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
        if (EndAlpha == 0f) //不要になったらenabledをオフにする？
        {
            fadeCanvas.enabled = false;
        }
    }

}
