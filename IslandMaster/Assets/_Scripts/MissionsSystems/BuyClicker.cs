using System;
using _Scripts.CharacterCore;
using _Scripts.InventorySystem;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class BuyClicker : MonoBehaviour
	{
		public GameObject itemToSell;
		public int amount;
		private Inventory _playerInventory;
		private PlayerBalance _playerBalance;

		private void Awake()
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory");
			_playerInventory = playerInventory.GetComponent<Inventory>();
			_playerBalance = player.GetComponent<PlayerBalance>();
		}

		public void OnClick()
		{
			if(amount <= _playerBalance._balanceAmount)
			{
				GameObject newItem = Instantiate(itemToSell);
				if(_playerInventory.AddItem(newItem.GetComponent<IInventoryItem>()))
				{
					_playerBalance.UpdateBalance(-amount);
				}
			}
		}
	}
}