using Kusume;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: リファクタリングする
namespace hikido
{
    public class HPManager : MonoBehaviour
    {

        //キャラクターの最大体力
        [SerializeField] private int charactorHP = 3;
        private int  currentHP = 0;
        private bool oneShotOut = false;
        private static bool endFlg = false;
        public static bool IsEndFlag => endFlg;

        [Header("HPImage")]
        [SerializeField] private Image HPImage;
        [SerializeField] private List<Sprite> hpSprite = new List<Sprite>();
        [SerializeField] private GameManagerSO _gameManagerSO;
        [SerializeField] private AndroidTypeController _controller;

        /*少し追加しました(by楠目)*/
        [SerializeField]
        private EntryResultPanel entryResultPanel;

        private void Awake()
        {
            entryResultPanel = FindObjectOfType<EntryResultPanel>();
        }

        private void Start()
        {
            entryResultPanel.gameObject.SetActive(false);

            currentHP = charactorHP;
            UpdateHelth();
        }

        private void OnEnable()
        {
            _gameManagerSO.OnAddDamage += TakeDamage;
            _gameManagerSO.MistakeDamage += MistakeImpact;
        }

        private void OnDisable()
        {
            _gameManagerSO.OnAddDamage -= TakeDamage;
            _gameManagerSO.MistakeDamage -= MistakeImpact;
        }
        
        private void Update()
        {
            //テスト用
            //TakeDamage(1); 
        }

        public void MistakeImpact()
        {
            TakeDamage(1);
        }

        public bool EndFlg()
        {
            return endFlg;
        }
       
        //ダメージ
        public void TakeDamage(int damage)
        {
            currentHP -= damage;
           
            //CurrentHPの数値の範囲を限定(HP上限 3,下限 0)
            currentHP = Mathf.Clamp(currentHP, 0, charactorHP);

            //ダメージを受けたとき->体力バー変化
            UpdateHelth();

    
            if(currentHP == 0)
            {
                endFlg = true;
                EndFlg();
                entryResultPanel.gameObject.SetActive(true);
            }
            
        }



        //HPバーの画像切り替え
        private void UpdateHelth()
        {
            if(HPImage == null)
            {
                Debug.LogError("Imageがありません。");
            }
            //現在の体力に合わせて画像を変更
            HPImage.sprite = hpSprite[currentHP];
        }


    }
       
}
