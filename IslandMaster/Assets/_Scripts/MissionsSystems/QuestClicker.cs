using System;
using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class QuestClicker : MonoBehaviour
	{
		public QuestInfo _Quest;
		public static event Action<QuestInfo> SendQuest;

		public void OnClick()
		{
			SendQuest?.Invoke(_Quest);
		}
	}
}