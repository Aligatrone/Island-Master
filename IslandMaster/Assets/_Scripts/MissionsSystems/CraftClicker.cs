using _Scripts.CharacterCore;
using _Scripts.InventorySystem;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
    public class CraftClicker : MonoBehaviour
    {
        public ItemCraft ItemCraft;
        private Inventory _playerInventory;
        
        private void Awake()
        {
            GameObject playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory");
            _playerInventory = playerInventory.GetComponent<Inventory>();
        }
        
        public void OnClick()
        {
            bool canCraft = true;
            
            foreach(var component in ItemCraft.itemsRecipe)
            {
                if(component.amount >
                   _playerInventory.FindNumberOfItems(component.component.GetComponent<IInventoryItem>()))
                    canCraft = false;
            }

            if(!canCraft) return;

            foreach(var component in ItemCraft.itemsRecipe)
            {
                for(int i = 0; i < component.amount; i++)
                    _playerInventory.RemoveItem(component.component.GetComponent<IInventoryItem>());
            }

            GameObject newItem = Instantiate(ItemCraft.item);
            _playerInventory.AddItem(newItem.GetComponent<IInventoryItem>());
        }
    }
}