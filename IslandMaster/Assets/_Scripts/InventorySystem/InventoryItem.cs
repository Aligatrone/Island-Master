using System;
using UnityEngine;

namespace _Scripts.InventorySystem
{
	public interface IInventoryItem
	{
		string Name { get; }
		Sprite Image { get; }
		int Amount { get; }

		void OnPickup();
		void OnUse();
		void OnRemove();
		void OnMove();
	}

	public class InventoryEventArgs : EventArgs
	{
		public IInventoryItem Item;
		
		public InventoryEventArgs(IInventoryItem item)
		{
			Item = item;
		}
	}
}