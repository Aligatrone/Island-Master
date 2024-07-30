using System;
using UnityEngine;

namespace _Scripts.Health
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected int maxHealth;

        public int Health => health;
        public int MaxHealth => maxHealth;

        public event Action OnEntityDamaged;
        public event Action OnEntityDeath;

        protected virtual void Awake()
        {
            health = maxHealth;
        }

        public virtual void TakeDamage(int amount)
        {
            health -= amount;

            if(health <= 0)
            {
                health = 0;
                OnEntityDeath?.Invoke();
            }
            else
                OnEntityDamaged?.Invoke();
        }
		
        public virtual void Heal(int amount)
        {
            health += amount;

            if(health > maxHealth)
                health = maxHealth;
        }
    }
}