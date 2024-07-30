using System;
using TMPro;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class CurrentQuestHolder : MonoBehaviour
	{
		private Quest _currentQuest;

		[SerializeField] private TMP_Text currentQuestText;
		[SerializeField] private GameObject currentQuestWindow;

		private void OnEnable()
		{
			QuestClicker.SendQuest += UpdateNewQuest;
			Quest.QuestFinished += CloseWindow;
		}

		private void Awake()
		{
			_currentQuest = GetComponent<Quest>();
		}

		private void OnDisable()
		{
			QuestClicker.SendQuest += UpdateNewQuest;
			Quest.QuestFinished -= CloseWindow;
		}

		private void UpdateNewQuest(QuestInfo questInfo)
		{
			currentQuestWindow.SetActive(true);
			_currentQuest.StartQuest(questInfo);
			currentQuestText.text = _currentQuest.QuestInfo.questText;
		}

		private void CloseWindow()
		{
			currentQuestWindow.SetActive(false);
		}
	}
}