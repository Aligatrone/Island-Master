using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class QuestsManager : MonoBehaviour
	{
		[SerializeField] private List<QuestInfo> listOfQuestsInfo = new();
		[SerializeField] private GameObject questPrefab;

		private void OnEnable()
		{
			QuestClicker.SendQuest += RemoveQuest;
		}

		private void OnDisable()
		{
			QuestClicker.SendQuest -= RemoveQuest;
		}

		private void RemoveQuest(QuestInfo questInfo)
		{
			listOfQuestsInfo.Remove(questInfo);
			DisplayQuests();
		}

		private void DisplayQuests()
		{
			
			CleanUp();
			
			foreach(var quest in listOfQuestsInfo)
			{
				if(quest == null) continue;
				
				GameObject newQuest = Instantiate(questPrefab, transform, true);
				
				IndividualQuest individualQuest = newQuest.GetComponent<IndividualQuest>();
				individualQuest.UpdateInfo(quest);
			}
		}

		private void Start()
		{
			DisplayQuests();
		}

		private void CleanUp()
		{
			foreach(Transform t in transform)
			{
				Destroy(t.gameObject);
			}
		}
	}
}