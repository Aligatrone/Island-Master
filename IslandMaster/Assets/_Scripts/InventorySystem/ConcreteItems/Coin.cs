using System;
using UnityEngine;

namespace _Scripts.InventorySystem.ConcreteItems
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int valueAmount = 1;
        [SerializeField] private float interactingRadius = 2;
        [SerializeField] private float closeEnough = 0.4f;
        [SerializeField] private float speed = 0.3f;
        [SerializeField] private float rotate = 1.0f;

        private GameObject _player;

        public static Action<int> AddCoinsToPlayer;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            transform.Rotate(0, rotate, 0, Space.Self);
            
            if(Vector3.Distance(_player.transform.position, transform.position) > interactingRadius) return;
            
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);

            if(Vector3.Distance(_player.transform.position, transform.position) < closeEnough)
            {
                AddCoinsToPlayer?.Invoke(valueAmount);
                Destroy(gameObject);
            }
        }
    }
}