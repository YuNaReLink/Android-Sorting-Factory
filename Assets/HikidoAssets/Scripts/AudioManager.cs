using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: リファクタリングする
namespace hikido
{
    public class AudioManager : MonoBehaviour
    {
        [Header("音源データ")]
        [SerializeField] private AudioSource SESource;
        [SerializeField] private AudioSource BGMSoruce;
        [SerializeField] private List<BGMSound> BGMSounds;
        [SerializeField] private List<SECategory> SECategorys;

        [Header("Volume管理")]
        [SerializeField] public float masterVolume = 1.0f;
        [SerializeField] public float bgmmasterVolume = 1.0f;
        [SerializeField] public float semasterVolume = 1.0f;


        #region singleton
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion


        /// <summary> /// BGMの停止 /// </summary>
        public void BGMStop()
        {
            BGMSoruce.Stop();
        }

        /// <summary> /// BGMの一時停止 /// </summary>
        public void BGMPause()
        {
            BGMSoruce.Pause();
        }

        /// <summary>　/// BGMの再開　/// </summary>
        public void BGMauPause()
        {
            BGMSoruce.UnPause();
        }


        /// <summary>
        /// BGMSoundのリスト内にあるBGMを再生
        /// </summary>
        /// <param name="bgm"></param>
        public void PlayBGM(BGMSound.BGMDETA bgm)
        {
            BGMSound data = BGMSounds.Find(data => data.bgmData == bgm);
            //データがnullどうかチェック
            if (data != null)
            {
                BGMSoruce.clip = data.bgmclip;
                BGMSoruce.volume = data.bgmVolume * masterVolume * bgmmasterVolume;
                BGMSoruce.Play();
            }
            else
            {
                //データの有無をチェック
                Debug.LogError("指定されたBGMデータが見つかりません:" + bgm);
            }

        }

        /// <summary>
        /// SEを再生
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="se"></param>
        public void PlaySE(string categoryName, SESound.SEDATA se)
        {
            SECategory category = SECategorys.Find(category => category.categoryName == categoryName);
            //categoryがnullかどうかチェック
            if (category != null)
            {
                SESound date = category.sounds.Find(sound => sound.seData == se);
                if (date != null)
                {
                    SESource.volume = date.seVolume * masterVolume * semasterVolume;
                    SESource.PlayOneShot(date.seclip);
                }
                else
                {
                    //SEの有無をチェック
                    Debug.LogError("指定されたSEが見つかりません:" + se);
                }
            }
            else
            {
                //カテゴリーの有無をチェック
                Debug.LogError("指定されたカテゴリが見つかりません。" + categoryName);
            }
        }

        /// <summary>
        /// SEカテゴリー内のSEを再生
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="index"></param>
        public void PlayspecificSE(string categoryName, int index)
        {
            SECategory category = SECategorys.Find(category => category.categoryName == categoryName);
            if (category != null)
            {
                if (index >= 0 && index < category.sounds.Count)
                {
                    SESound data = category.sounds[index];
                    SESource.volume = data.seVolume * masterVolume * semasterVolume;
                    SESource.PlayOneShot(data.seclip);
                }
                else
                {
                    Debug.LogError("指定されたSEのインデックスが見つかりません。" + index + categoryName);
                }
            }
            else
            {
                Debug.LogError("指定されたカテゴリが見つかりません。" + categoryName);
            }
        }



    }

    /// <summary>/// BGMデータの管理　/// </summary>
    [Serializable]
    public class BGMSound
    {
        public enum BGMDETA
        {
            Title,
            Ingame,
            Result,
            None,
        }

        public BGMDETA bgmData;
        public AudioClip bgmclip;
        [Range(0f, 1f)] public float bgmVolume = 1;

    }

    /// <summary> /// SEの音源データ管理 /// </summary>
    [Serializable]
    public class SESound
    {
        public enum SEDATA
        {
            character,
            System,
            None,
        }
        public SEDATA seData;
        public AudioClip seclip;
        [Range(0f, 1f)] public float seVolume = 1;


    }


    /// <summary>　/// SEをカテゴリーごとに管理　/// </summary>
    [Serializable]
    public class SECategory
    {
        //カテゴリ名で管理
        public string categoryName;
        public List<SESound> sounds = new List<SESound>();
    }

}

