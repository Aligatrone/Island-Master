using _Scripts.InventorySystem.ConcreteItems;
using TMPro;
using UnityEngine;

namespace _Scripts.CharacterCore
{
	public class PlayerBalance : MonoBehaviour
	{
		public int _balanceAmount;
		public int _numberOfQuestsDone;
		[SerializeField] private TMP_Text balanceText;
    
		private void Start()
		{
			_balanceAmount = 0;
			Coin.AddCoinsToPlayer += UpdateBalance;
		}

		public void UpdateBalance(int amount)
		{
			_balanceAmount += amount;
			balanceText.text = _balanceAmount.ToString();
		}

		public void IncrementQuest()
		{
			_numberOfQuestsDone += 1;
		}

		public bool HasEnoughBalance(int amount)
		{
			return amount <= _balanceAmount;
		}
	}
}