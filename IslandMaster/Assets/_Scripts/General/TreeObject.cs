using _Scripts.Health;
using UnityEngine;

namespace _Scripts.General
{
	public class TreeObject : MonoBehaviour
	{
		private HealthSystem _healthSystem;
		[SerializeField] private GameObject loot;
		[SerializeField] private float radius = 1.0f;
	
		private void OnEnable()
		{
			_healthSystem.OnEntityDeath += OnDeath;
		}

		private void OnDisable()
		{
			_healthSystem.OnEntityDeath -= OnDeath;
		}
	
		private void Awake()
		{
			var rotate = Random.Range(0, 360);
			transform.Rotate(0, 0, rotate, Space.Self);
			_healthSystem = GetComponent<HealthSystem>();
		}
	
		private void OnDeath()
		{
			for(int i = 0; i < 2; i++)
			{
				Vector2 randomLocation = Random.insideUnitCircle * radius;
				Instantiate(loot, new Vector3(randomLocation.x + transform.position.x, transform.position.y, randomLocation.y + transform.position.z), Quaternion.identity);
			}
            
			Destroy(gameObject);
		}
	}
}