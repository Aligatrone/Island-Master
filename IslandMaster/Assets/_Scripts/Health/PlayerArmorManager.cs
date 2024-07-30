using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Health
{
    public class PlayerArmorManager : MonoBehaviour
    {
        public GameObject armorPrefab;
        public PlayerHealth playerHealth;

        private List<HealthHeart> _armor = new();

        private void OnEnable()
        {
        	playerHealth.UpdateArmor += DrawArmor;
        }

        private void OnDisable()
        {
        	playerHealth.UpdateArmor -= DrawArmor;
        }

        public void Start()
        {
        	DrawArmor();
        }

        private void DrawArmor()
        {
        	ClearArmor();

        	float maxArmorReminder = playerHealth.MaxArmor % 2;
        	var numberOfArmor = (int)(playerHealth.MaxArmor / 2 + maxArmorReminder);

        	for(int i = 0; i < numberOfArmor; i++)
        	{
        		CreateEmptyArmor();
        	}

        	for(int i = 0; i < _armor.Count; i++)
        	{
        		int heartStatusReminder = Mathf.Clamp(playerHealth.Armor - (i * 2), 0, 2);
        		
        		_armor[i].SetHeartImage((HeartStatus)heartStatusReminder);
        	}
        }

        private void CreateEmptyArmor()
        {
        	GameObject newArmor = Instantiate(armorPrefab, transform, true);

        	HealthHeart healthComponent = newArmor.GetComponent<HealthHeart>();
        	healthComponent.SetHeartImage(HeartStatus.Empty);
        	
        	_armor.Add(healthComponent);
        }

        private void ClearArmor()
        {
        	foreach(Transform t in transform)
        	{
        		Destroy(t.gameObject);
        	}

        	_armor = new List<HealthHeart>();
        }
    }
}