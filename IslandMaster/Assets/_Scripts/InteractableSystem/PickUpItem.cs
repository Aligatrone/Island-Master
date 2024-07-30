using System;
using _Scripts.InventorySystem;

namespace _Scripts.InteractableSystem
{
    public class PickUpItem : Interactable
    {
        private IInventoryItem _item;
        public static event EventHandler<InventoryEventArgs> AddItemToInventory;
 
        public override void Start()
        {
            base.Start();
            _item = gameObject.GetComponent<IInventoryItem>();
        }
 
        protected override void Interaction()
        {
            AddItemToInventory?.Invoke(this, new InventoryEventArgs(_item));
        }
 
    }
}