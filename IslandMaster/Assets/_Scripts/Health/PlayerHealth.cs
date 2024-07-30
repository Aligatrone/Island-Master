using System;
using System.Collections;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ConcreteItems;
using UnityEngine;

namespace _Scripts.Health
{
	public class PlayerHealth : HealthSystem
	{
		public event Action UpdateHealth;
		public event Action UpdateArmor;
		public bool isImmune;

		[SerializeField] public int armor;
		[SerializeField] public int maxArmor;
		private bool _canRegenerateArmor = true;
		[SerializeField] private float timeToRegenerateAfterHit = 10.0f;
		private float _timePassed;

		public int MaxArmor
		{
			get => maxArmor;
			set => maxArmor = value;
		}

		public int Armor => armor;

		protected override void Awake()
		{
			base.Awake();

			armor = maxArmor;
			HealingPotion.HealPlayer += Heal;
			UpdateHealth?.Invoke();
			UpdateArmor?.Invoke();
			StartCoroutine("IncreaseArmor");
		}

		public override void TakeDamage(int amount)
		{
			if(isImmune) return;

			if(amount > 0)
			{
				_canRegenerateArmor = false;
				_timePassed = 0.0f;
			}

			if(armor >= amount)
			{
				armor -= amount;
				UpdateArmor?.Invoke();
				base.TakeDamage(0);
			}
			else
			{
				amount -= armor;
				armor = 0;
				base.TakeDamage(amount);
				UpdateHealth?.Invoke();
				UpdateArmor?.Invoke();
			}
		}

		public void UpdateMaxArmor(int amount)
		{
			maxArmor += amount;

			if(armor >= maxArmor)
				armor = maxArmor;
		}
		
		public override void Heal(int amount)
		{
			base.Heal(amount);
			
			UpdateHealth?.Invoke();
		}

		public void Revive()
		{
			health = maxHealth;
			armor = maxArmor;
			
			UpdateHealth?.Invoke();
			UpdateArmor?.Invoke();
		}

		private void Update()
		{
			if(!_canRegenerateArmor)
			{
				_timePassed += Time.deltaTime;

				if(_timePassed >= timeToRegenerateAfterHit)
					_canRegenerateArmor = true;
			}
		}

		IEnumerator IncreaseArmor() {
			for(;;)
			{
				if(_canRegenerateArmor)
				{
					armor += 1;
					if(armor >= maxArmor)
						armor = maxArmor;
				
					UpdateArmor?.Invoke();
				}
				yield return new WaitForSeconds(1.0f);
			}
		}
	}
}