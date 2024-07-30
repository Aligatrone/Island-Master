using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace _Scripts.InventorySystem
{
	public class SlotsManager : MonoBehaviour
	{
		public GameObject slotPrefab;
		public Inventory playerInventory;

		private List<GameObject> _slots = new();

		private void Start()
		{
			DrawSlots();
		}

		private void OnEnable()
		{
			playerInventory.ItemAction += DrawSlots;
		}

		private void OnDisable()
		{
			playerInventory.ItemAction -= DrawSlots;
		}

		private void DrawSlots()
		{
			ClearSlots();

			for(int i = 0; i < playerInventory.Slots.Count; i++)
			{
				CreateSlot();
			}

			for(int i = 0; i < _slots.Count; i++)
			{
				GameObject currentSlot = _slots[i];
				IInventoryItem slotItem = playerInventory.Slots[i].FirstItem;
				
				if(slotItem == null) continue;

				currentSlot.GetComponent<ItemClickHandler>().item = slotItem;
				Transform currentSlotTransform = currentSlot.transform;
				
				Transform imageTransform = currentSlotTransform.GetChild(0);
				Transform countBackground = currentSlotTransform.GetChild(1);
				Transform textTransform = currentSlotTransform.GetChild(2);
				Image itemImage = imageTransform.GetComponent<Image>();
				Image countImage = countBackground.GetComponent<Image>();
				TMP_Text txtCount = textTransform.GetComponent<TMP_Text>();
				
				itemImage.enabled = true;
				itemImage.sprite = slotItem.Image;
				int itemCount = playerInventory.Slots[i].Count;

				if(itemCount > 1)
				{
					txtCount.text = itemCount.ToString();
					countImage.enabled = true;
				}
				else txtCount.text = "";
			}
		}

		private void CreateSlot()
		{
			GameObject newSlot = Instantiate(slotPrefab, transform, true);
			_slots.Add(newSlot);
		}

		private void ClearSlots()
		{
			foreach(Transform t in transform)
			{
				Destroy(t.gameObject);
			}

			_slots = new List<GameObject>();
		}
	}
}