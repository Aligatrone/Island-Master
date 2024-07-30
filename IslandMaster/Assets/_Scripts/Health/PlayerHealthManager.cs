using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Health
{
	public class HealthManager : MonoBehaviour
	{
		public GameObject heartPrefab;
		public PlayerHealth playerHealth;

		private List<HealthHeart> _hearts = new();

		private void OnEnable()
		{
			playerHealth.UpdateHealth += DrawHearts;
		}

		private void OnDisable()
		{
			playerHealth.UpdateHealth -= DrawHearts;
		}

		public void Start()
		{
			DrawHearts();
		}

		private void DrawHearts()
		{
			ClearHearts();

			float maxHealthReminder = playerHealth.MaxHealth % 2;
			var numberOfHearts = (int)(playerHealth.MaxHealth / 2 + maxHealthReminder);

			for(int i = 0; i < numberOfHearts; i++)
			{
				CreateEmptyHeart();
			}

			for(int i = 0; i < _hearts.Count; i++)
			{
				int heartStatusReminder = Mathf.Clamp(playerHealth.Health - (i * 2), 0, 2);
				
				_hearts[i].SetHeartImage((HeartStatus)heartStatusReminder);
			}
		}

		private void CreateEmptyHeart()
		{
			GameObject newHeart = Instantiate(heartPrefab, transform, true);

			HealthHeart healthComponent = newHeart.GetComponent<HealthHeart>();
			healthComponent.SetHeartImage(HeartStatus.Empty);
			
			_hearts.Add(healthComponent);
		}

		private void ClearHearts()
		{
			foreach(Transform t in transform)
			{
				Destroy(t.gameObject);
			}

			_hearts = new List<HealthHeart>();
		}
	}
}