using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.CharacterCore.CharacterSM;
using _Scripts.Health;
using _Scripts.InventorySystem.ConcreteItems;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Scripts.EnemyCore
{
	public class Enemy : MonoBehaviour
	{
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float aggroRange = 4f;
        [SerializeField] private List<Coin> loot = new();
        [SerializeField] private float radius = 1.0f;
        [SerializeField] private string monsterName;
        
        public static event Action<string> QuestObjective;
     
        private GameObject _player;
        private CharacterStateMachine _playerSm;
        private Animator _animator;
        private NavMeshAgent _agent;
        private HealthSystem _healthSystem;
        private bool _isDead;
        private bool _aggro;

        private float _timePassed;
        private float _newDestinationCooldown = 0.5f;
        
        private static readonly int Damage = Animator.StringToHash("Damage");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void OnEnable()
        {
            _healthSystem.OnEntityDamaged += TakeDamage;
            _healthSystem.OnEntityDeath += OnDeath;
        }

        private void OnDisable()
        {
            _healthSystem.OnEntityDamaged -= TakeDamage;
            _healthSystem.OnEntityDeath -= OnDeath;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerSm = _player.GetComponent<CharacterStateMachine>();
            _healthSystem = GetComponent<HealthSystem>();
        }

        private void Update()
        {
            if(_isDead) return;

            _animator.SetFloat(Speed, _agent.velocity.magnitude / _agent.speed);
            
            if(!_player || _playerSm.CurrentState is CharacterDeathState)
            {
                return;
            }
            
            if(_timePassed >= attackCooldown)
            {
                if(Vector3.Distance(_player.transform.position, transform.position) <= attackRange)
                {
                    _animator.SetTrigger(Attack);
                    _timePassed = 0;
                }
            }
            _timePassed += Time.deltaTime;
 
            if(_newDestinationCooldown <= 0 && Vector3.Distance(_player.transform.position, transform.position) <= aggroRange)
            {
                _newDestinationCooldown = 0.5f;
                _agent.SetDestination(_player.transform.position);
            }
            
            _newDestinationCooldown -= Time.deltaTime;
            transform.LookAt(_player.transform);
        }

        private void TakeDamage()
        {
            if(_isDead) return;
            
            _animator.SetTrigger(Damage);
        }

        private void OnDeath()
        {
            if(_isDead) return;
            _isDead = true;
            _animator.SetTrigger(Dead);
            StartCoroutine(WaitDeath());
            QuestObjective?.Invoke(monsterName);
        }

        IEnumerator WaitDeath()
        {
            yield return new WaitForSeconds(5);

            for(int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, loot.Count);
                Vector2 randomLocation = Random.insideUnitCircle * radius;
                Instantiate(loot[randomIndex], new Vector3(randomLocation.x + transform.position.x, transform.position.y, randomLocation.y + transform.position.z), Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
        
        public void StartDealDamage()
        {
            GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
        }
        
        public void EndDealDamage()
        {
            GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
	}
}