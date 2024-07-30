using _Scripts.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MissionsSystems
{
    public class CraftItemHolder : MonoBehaviour
    {
        private ItemCraft _itemCraft;

        [SerializeField] private CraftClicker craftClicker;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemAmount;
        [SerializeField] private GameObject itemAmountBorder;
        [SerializeField] private GameObject itemsNeeded;
        [SerializeField] private GameObject componentPrefab;
        
        public void SetCraftItem(ItemCraft itemInfo)
        {
            _itemCraft = itemInfo;
            craftClicker.ItemCraft = _itemCraft;
            itemIcon.sprite = _itemCraft.item.GetComponent<IInventoryItem>().Image;
            itemAmount.text = _itemCraft.amount > 1 ? _itemCraft.amount.ToString() : "";
            itemAmountBorder.SetActive(_itemCraft.amount > 1);
            
            foreach(var component in _itemCraft.itemsRecipe)
            {
                GameObject newComponent = Instantiate(componentPrefab, itemsNeeded.transform, true);
				
                ItemComponentHolder componentHolder = newComponent.GetComponent<ItemComponentHolder>();
                componentHolder.SetComponent(component.component, component.amount);
            }
        }
    }
}