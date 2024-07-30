using System;
using _Scripts.InteractableSystem;
using UnityEngine;

namespace _Scripts.InventorySystem
{
	public class InventoryItemBase : MonoBehaviour, IInventoryItem
	{
		public static Action<IInventoryItem> ItemRemoved;
		public static event Action<string> QuestObjective;

		public string _Name = null;
		public virtual string Name => _Name;

		public Sprite _Image = null;
		public Sprite Image => _Image;

		public int _Amount = 0;
		public int Amount => _Amount;


		public virtual void OnPickup()
		{
			gameObject.SetActive(false);
			gameObject.layer = LayerMask.NameToLayer("Default");
			if(gameObject.TryGetComponent(out Interactable itemInteractable))
			{
				itemInteractable.enabled = false;
			}
			QuestObjective?.Invoke(Name);
		}

		public InventorySlot Slot { get; set; }
		
		public virtual void OnUse()
		{
			
		}

		public void OnMove()
		{
			
		}

		public virtual void OnRemove()
		{
			ItemRemoved?.Invoke(this);
		}

		public Vector3 pickPosition;
		public Vector3 pickRotation;
		public Vector3 dropRotation;
	}
}