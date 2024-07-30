using _Scripts.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MissionsSystems
{
    public class ItemComponentHolder : MonoBehaviour
    {
        private GameObject _itemComponent;
        private int _amount;
        
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemAmount;
        [SerializeField] private GameObject itemAmountBorder;

        public void SetComponent(GameObject itemComponent, int amount)
        {
            _itemComponent = itemComponent;
            _amount = amount;
            itemIcon.sprite = _itemComponent.GetComponent<IInventoryItem>().Image;
            itemAmount.text = _amount > 1 ? _amount.ToString() : "";
            itemAmountBorder.SetActive(_amount > 1);
        }
        
        
    }
}