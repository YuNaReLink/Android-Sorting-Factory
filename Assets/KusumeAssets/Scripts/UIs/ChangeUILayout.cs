using UnityEngine;
using UnityEngine.UI;

namespace Kusume
{
    public class ChangeUILayout : MonoBehaviour
    {
        private Button      button;

        [SerializeField]
        private ChangeToUI  changeToUI;

        private void Awake()
        {
            button = GetComponent<Button>();
            changeToUI = FindObjectOfType<ChangeToUI>();
        }

        private void Start()
        {
            button.onClick.AddListener(ChangeUI);
            changeToUI.gameObject.SetActive(false);
        }


        private void ChangeUI()
        {
            gameObject.SetActive(false);
            changeToUI.gameObject.SetActive(true);
        }
    }
}
