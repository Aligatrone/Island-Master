using TMPro;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class IndividualQuest : MonoBehaviour
	{
		private QuestInfo _questInfo;
		
		[SerializeField] private TMP_Text individualQuestText;
		[SerializeField] private QuestClicker individualQuestWindow;

		public void UpdateInfo(QuestInfo questInfo)
		{
			_questInfo = questInfo;
			individualQuestText.text = _questInfo.questText;
			individualQuestWindow._Quest = _questInfo;
		}
	}
}