using _Scripts.Health;
using UnityEngine;

namespace _Scripts.EnemyCore
{
    public class EnemyDamageDealer : MonoBehaviour
    {
        private bool _canDealDamage;
        private bool _hasDealtDamage;
 
        [SerializeField] private float weaponLength;
        [SerializeField] private int weaponDamage;

        private void Start()
        {
            _canDealDamage = false;
            _hasDealtDamage = false;
        }
        
        private void Update()
        {
            if(!_canDealDamage || _hasDealtDamage) return;

            const int layerMask = 1 << 8;
            
            if (Physics.Raycast(transform.position, -transform.up, out var hit, weaponLength, layerMask))
            {
                if(!hit.transform.TryGetComponent(out HealthSystem health)) return;
                
                health.TakeDamage(weaponDamage);
                _hasDealtDamage = true;
            }
        }
        
        public void StartDealDamage()
        {
            _canDealDamage = true;
            _hasDealtDamage = false;
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