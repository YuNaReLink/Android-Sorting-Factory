using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ���t�@�N�^�����O����
namespace hikido
{
    public class HPManager : MonoBehaviour
    {
        [SerializeField] private int CharactorHP = 0;
        [SerializeField] private GameObject HeartObj;

        /// <summary> /// ���X�g�Ƃ��ĊǗ� /// </summary>
        private List<GameObject> playerHeartList = new List<GameObject>();

        private void Start()
        {
            CreateHeart();
        }

        /// <summary> /// �Q�[���J�n���Ƀn�[�g�𐶐� /// </summary>
        private void CreateHeart()
        {
            // �����̃n�[�g�A�C�R�������ׂč폜
            foreach (var heart in playerHeartList)
            {
                Destroy(heart);
            }
            playerHeartList.Clear();

            // �ő�̗͕��̃n�[�g�A�C�R���𐶐�
            for (int i = 0; i < CharactorHP; i++)
            {
                GameObject heart = Instantiate(HeartObj);
                heart.transform.parent = transform;
                playerHeartList.Add(heart);
            }
        }

        /// <summary> /// �n�[�g�̕\���E��\�� /// </summary>
        private void AstrengthDisplay()
        {
            // ���ׂẴn�[�g�A�C�R�����\���ɂ���
            foreach (var heart in playerHeartList)
            {
                heart.SetActive(false);
            }

            // ���݂̗͕̑��̃n�[�g�A�C�R����\��
            for (int i = 0; i < CharactorHP; i++)
            {
                if (i < playerHeartList.Count) // �z��͈͓̔����m�F
                {
                    playerHeartList[i].SetActive(true);
                }
            }
        }

    }

}
