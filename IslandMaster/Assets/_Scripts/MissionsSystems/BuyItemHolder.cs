using _Scripts.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MissionsSystems
{
    public class BuyItemHolder : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int amount;

        [SerializeField]
        private BuyClicker buyButton;

        [SerializeField]
        private Image ItemIcon;

        [SerializeField] private TMP_Text ItemPrice;

        private void Setup()
        {
            buyButton.amount = amount;
            buyButton.itemToSell = prefab;
            ItemIcon.sprite = prefab.GetComponent<IInventoryItem>().Image;
            ItemPrice.text = amount.ToString();
        }

        public void SetBuyItem(GameObject _prefab, int _amount)
        {
            prefab = _prefab;
            amount = _amount;
            
            Setup();
        }
    }
}