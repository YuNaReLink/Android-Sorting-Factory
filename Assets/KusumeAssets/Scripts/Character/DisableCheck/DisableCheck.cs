using hikido;
using System;
using UnityEngine;

namespace Kusume
{
    public class DisableCheck : MonoBehaviour
    {
        [SerializeField] 
        private GameManagerSO _gameManagerSO;

        public event Action<bool> OnFinishEvent;

        public void OutScreenCheck(AndroidTypeController controller)
        {
            Vector3 screenMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 screenMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            if (transform.position.x < screenMin.x - 2f || transform.position.x > screenMax.x ||
                transform.position.y < screenMin.y || transform.position.y > screenMax.y)
            {
                Disable();
                Debug.Log("対象は画面外に出た");

                if (transform.position.x > screenMax.x)
                {
                    switch (controller.Type)
                    {
                        case AndroidType.Normal:
                            GameManager.AddNormalAndroidNumber();
                            //スコア追加
                            _gameManagerSO.OnAddScore?.Invoke();
                            break;
                        case AndroidType.Bad:
                            _gameManagerSO.OnAddDamage?.Invoke(1);
                            break;
                        case AndroidType.VeryBad:
                            _gameManagerSO.OnAddDamage?.Invoke(3);
                            break;
                    }
                }
            }
        }

        public void BadDamageCheck(AndroidTypeController controller)
        {
            switch (controller.Type)
            {
                case AndroidType.Normal:
                case AndroidType.Bad:
                case AndroidType.VeryBad:
                    _gameManagerSO.MistakeDamage?.Invoke();
                    break;
            }
        }

        public void NormalDamageCheck(AndroidTypeController controller)
        {
            switch (controller.Type)
            {
                case AndroidType.Normal:
                    _gameManagerSO.MistakeDamage?.Invoke();
                    break;
                case AndroidType.Bad:
                case AndroidType.VeryBad:
                    //スコア追加
                    _gameManagerSO.OnAddScore?.Invoke();
                    GameManager.AddBadAndroidNumber();
                    break;
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            OnFinishEvent?.Invoke(false);
        }
    }
}
