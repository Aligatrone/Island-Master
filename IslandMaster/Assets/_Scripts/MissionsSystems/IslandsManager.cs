using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MissionsSystems
{
	public class IslandsManager : MonoBehaviour
	{
		[SerializeField] private List<IslandData> islandsInfo;
		[SerializeField] private GameObject islandDisplayPrefab;

		private void Awake()
		{
			foreach(IslandData island in islandsInfo)
			{
				GameObject newComponent = Instantiate(islandDisplayPrefab, transform, true);

				Image componentImage = newComponent.GetComponent<Image>();
				componentImage.sprite = island.islandSprite;

				IslandHandler islandHandler = newComponent.GetComponentInChildren<IslandHandler>();
				islandHandler.Setup(island);
			}
		}
	}

	[Serializable]
	public struct IslandData
	{
		public Sprite islandSprite;
		public int amount;
		public int numberOfQuests;
		public GameObject teleportAt;
	}
}