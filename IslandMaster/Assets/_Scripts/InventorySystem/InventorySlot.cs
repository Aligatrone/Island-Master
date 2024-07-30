using System.Collections.Generic;

namespace _Scripts.InventorySystem
{
    public class InventorySlot
    {
        private int _maxAmount;
        private readonly Stack<IInventoryItem> _inventoryItems = new();

        private readonly int _id;

        public InventorySlot(int id)
        {
            _id = id;
        }

        public int Id => _id;

        public void AddItem(IInventoryItem item)
        {
            _maxAmount = item.Amount;
            _inventoryItems.Push(item);
        }

        public IInventoryItem FirstItem => IsEmpty ? null : _inventoryItems.Peek();

        public bool IsStackable(IInventoryItem item)
        {
            if(IsEmpty)
                return false;

            // if(_inventoryItems.Count >= _maxAmount)
            //     return false;

            var first = _inventoryItems.Peek();

            return first.Name == item.Name;
        }

        public bool IsEmpty => Count == 0;

        public int Count => _inventoryItems.Count;

        public bool Remove(IInventoryItem item)
        {
            if(IsEmpty) return false;

            var first = _inventoryItems.Peek();

            if(first.Name != item.Name) return false;
            
            _inventoryItems.Pop();
            return true;
        }
    }
}