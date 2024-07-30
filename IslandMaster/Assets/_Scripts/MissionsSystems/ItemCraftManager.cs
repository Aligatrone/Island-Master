using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class ItemCraftManager : MonoBehaviour
	{
		[SerializeField] private List<ItemCraft> itemsToCraft = new();
		[SerializeField] private GameObject CraftSlotPrefab;

		private void Awake()
		{
			foreach(var item in itemsToCraft)
			{
				GameObject newQuest = Instantiate(CraftSlotPrefab, transform, true);
				
				CraftItemHolder itemHolder = newQuest.GetComponent<CraftItemHolder>();
				itemHolder.SetCraftItem(item);
			}
		}
	}

	[Serializable]
	public struct ItemCraft
	{
		public GameObject item;
		public int amount;
		public List<CraftComponent> itemsRecipe;
	}

	[Serializable]
	public struct CraftComponent
	{
		public GameObject component;
		public int amount;
	}
}