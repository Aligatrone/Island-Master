using _Scripts.CharacterCore;
using UnityEngine;

public class IslandClicker : MonoBehaviour
{
	public int amount;
	public int quests;

	private PlayerBalance _playerBalance;
	[SerializeField] private GameObject tpButton;

	private void Awake()
	{
		GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
		_playerBalance = playerGameObject.GetComponent<PlayerBalance>();
	}

	public void OnClick()
	{
		if(_playerBalance._balanceAmount >= amount && quests <= _playerBalance._numberOfQuestsDone)
		{
			_playerBalance.UpdateBalance(-amount);
			tpButton.SetActive(true);
			transform.parent.gameObject.SetActive(false);
		}
	}
}