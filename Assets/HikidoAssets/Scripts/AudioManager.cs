using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ���t�@�N�^�����O����
namespace hikido
{
    public class AudioManager : MonoBehaviour
    {
        [Header("�����f�[�^")]
        [SerializeField] private AudioSource SESource;
        [SerializeField] private AudioSource BGMSoruce;
        [SerializeField] private List<BGMSound> BGMSounds;
        [SerializeField] private List<SECategory> SECategorys;

        [Header("Volume�Ǘ�")]
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


        /// <summary> /// BGM�̒�~ /// </summary>
        public void BGMStop()
        {
            BGMSoruce.Stop();
        }

        /// <summary> /// BGM�̈ꎞ��~ /// </summary>
        public void BGMPause()
        {
            BGMSoruce.Pause();
        }

        /// <summary>�@/// BGM�̍ĊJ�@/// </summary>
        public void BGMauPause()
        {
            BGMSoruce.UnPause();
        }


        /// <summary>
        /// BGMSound�̃��X�g���ɂ���BGM���Đ�
        /// </summary>
        /// <param name="bgm"></param>
        public void PlayBGM(BGMSound.BGMDETA bgm)
        {
            BGMSound data = BGMSounds.Find(data => data.bgmData == bgm);
            //�f�[�^��null�ǂ����`�F�b�N
            if (data != null)
            {
                BGMSoruce.clip = data.bgmclip;
                BGMSoruce.volume = data.bgmVolume * masterVolume * bgmmasterVolume;
                BGMSoruce.Play();
            }
            else
            {
                //�f�[�^�̗L�����`�F�b�N
                Debug.LogError("�w�肳�ꂽBGM�f�[�^��������܂���:" + bgm);
            }

        }

        /// <summary>
        /// SE���Đ�
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="se"></param>
        public void PlaySE(string categoryName, SESound.SEDATA se)
        {
            SECategory category = SECategorys.Find(category => category.categoryName == categoryName);
            //category��null���ǂ����`�F�b�N
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
                    //SE�̗L�����`�F�b�N
                    Debug.LogError("�w�肳�ꂽSE��������܂���:" + se);
                }
            }
            else
            {
                //�J�e�S���[�̗L�����`�F�b�N
                Debug.LogError("�w�肳�ꂽ�J�e�S����������܂���B" + categoryName);
            }
        }

        /// <summary>
        /// SE�J�e�S���[����SE���Đ�
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
                    Debug.LogError("�w�肳�ꂽSE�̃C���f�b�N�X��������܂���B" + index + categoryName);
                }
            }
            else
            {
                Debug.LogError("�w�肳�ꂽ�J�e�S����������܂���B" + categoryName);
            }
        }



    }

    /// <summary>/// BGM�f�[�^�̊Ǘ��@/// </summary>
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

    /// <summary> /// SE�̉����f�[�^�Ǘ� /// </summary>
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


    /// <summary>�@/// SE���J�e�S���[���ƂɊǗ��@/// </summary>
    [Serializable]
    public class SECategory
    {
        //�J�e�S�����ŊǗ�
        public string categoryName;
        public List<SESound> sounds = new List<SESound>();
    }

}

