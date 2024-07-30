using System.Collections.Generic;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.CharacterCore
{
	public class DamageDealer : MonoBehaviour
	{
		private bool _canDealDamage;
		private List<GameObject> _hasDealtDamage;

		[SerializeField] private float weaponLength;
		[SerializeField] private int weaponDamage;

		private void Start()
		{
			_canDealDamage = false;
			_hasDealtDamage = new List<GameObject>();
		}

		private void Update()
		{
			if(!_canDealDamage) return;

			const int layerMask = 1 << 9;

			if(Physics.Raycast(transform.position, -transform.up, out var hit, weaponLength, layerMask))
			{
				if(!hit.transform.TryGetComponent(out HealthSystem enemyHealthSystem)) return;
				
				if(_hasDealtDamage.Contains(hit.transform.gameObject)) return;

				enemyHealthSystem.TakeDamage(weaponDamage);
				_hasDealtDamage.Add(hit.transform.gameObject);
			}
		}
		
		public void StartDealDamage()
		{
			_canDealDamage = true;
			_hasDealtDamage.Clear();
		}
	
		public void EndDealDamage()
		{
			_canDealDamage = false;
		}
 
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
		}
	}
}