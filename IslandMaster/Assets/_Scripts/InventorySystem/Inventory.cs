using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.InteractableSystem;
using UnityEngine;

namespace _Scripts.InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] private int SLOTS;
		private readonly IList<InventorySlot> _slots = new List<InventorySlot>();

		public event Action ItemAction;

		private void OnEnable()
		{
			PickUpItem.AddItemToInventory += AddItem;
			InventoryItemBase.ItemRemoved += RemoveItem;
			
			for(var i = 0; i < SLOTS; i++)
			{
				_slots.Add(new InventorySlot(i));
			}
		}

		private void OnDisable()
		{
			PickUpItem.AddItemToInventory -= AddItem;
			InventoryItemBase.ItemRemoved -= RemoveItem;
		}

		public IList<InventorySlot> Slots => _slots;

		private InventorySlot FindStackableSlot(IInventoryItem item)
		{
			foreach(var slot in _slots)
			{
				if(slot.IsStackable(item))
				{
					return slot;
				}
					
			}

			return null;
		}

		private InventorySlot FindNextEmptySlot()
		{
			return _slots.FirstOrDefault(slot => slot.IsEmpty);
		}

		private void AddItem(object sender, InventoryEventArgs e)
		{
			var item = e.Item;
			
			var freeSlot = FindStackableSlot(item) ?? FindNextEmptySlot();
			
			if(freeSlot == null) return;
			
			item.OnPickup();
			freeSlot.AddItem(item);
			ItemAction?.Invoke();
		}

		public bool AddItem(IInventoryItem item)
		{
			var freeSlot = FindStackableSlot(item) ?? FindNextEmptySlot();
			
			if(freeSlot == null) return false;
			
			item.OnPickup();
			freeSlot.AddItem(item);
			ItemAction?.Invoke();
			return true;
		}

		public void RemoveItem(IInventoryItem item)
		{
			foreach(var slot in _slots)
			{
				if(slot.Remove(item))
				{
					ItemAction?.Invoke();
					break;
				}
			}
		}

		public int FindNumberOfItems(IInventoryItem item)
		{
			int numberOfItems = 0;
			
			foreach(var slot in _slots)
			{
				if(slot.IsStackable(item))
					numberOfItems += slot.Count;
			}

			return numberOfItems;
		}

		public void UseItem(IInventoryItem item)
		{
			ItemAction?.Invoke();
		}
	}
}