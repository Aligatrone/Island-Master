using System;
using System.Collections.Generic;
using _Scripts.MissionsSystems;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
	[SerializeField] private GameObject itemShopPrefab;
	[SerializeField] private List<ItemBuy> _shopItems = new();

	private void Awake()
	{
		foreach(var item in _shopItems)
		{
			GameObject newQuest = Instantiate(itemShopPrefab, transform, true);
				
			BuyItemHolder itemHolder = newQuest.GetComponent<BuyItemHolder>();
			itemHolder.SetBuyItem(item.itemPrefab, item.amount);
		}
	}
}

[Serializable]
public struct ItemBuy
{
	public GameObject itemPrefab;
	public int amount;
}