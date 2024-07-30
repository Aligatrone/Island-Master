using _Scripts.MissionsSystems;
using TMPro;
using UnityEngine;

public class IslandHandler : MonoBehaviour
{
	private IslandData _islandData;
	[SerializeField] private TMP_Text AmountText;
	[SerializeField] private TMP_Text QuestsText;
	[SerializeField] private IslandClicker _islandClicker;
	[SerializeField] private TpClicker _tpClicker;

	public void Setup(IslandData islandData)
	{
		_islandData = islandData;
		AmountText.text = _islandData.amount.ToString();
		QuestsText.text = _islandData.numberOfQuests.ToString();
		_islandClicker.amount = _islandData.amount;
		_islandClicker.quests = _islandData.numberOfQuests;
		_tpClicker.tpLocation = _islandData.teleportAt;
	}
}