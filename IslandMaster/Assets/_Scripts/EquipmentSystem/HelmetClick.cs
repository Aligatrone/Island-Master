using _Scripts.Health;
using _Scripts.InventorySystem.ConcreteItems;
using UnityEngine;

namespace _Scripts.EquipmentSystem
{
    public class HelmetClick : EquipmentClick
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        private void OnEnable()
        {
            Helmet.HelmetEquiped += OnWeaponEquiped;
        }

        private void OnDisable()
        {
            Helmet.HelmetEquiped -= OnWeaponEquiped;
        }
        
        protected override void OnWeaponEquiped(GameObject weapon)
        {
            if(mainWeapon != null)
                _playerHealth.UpdateMaxArmor(-mainWeapon.GetComponent<Helmet>().armorAmount);
            
            base.OnWeaponEquiped(weapon);
            
            _playerHealth.UpdateMaxArmor(mainWeapon.GetComponent<Helmet>().armorAmount);
            playerEquipmentSystem.AddHelmet(mainWeapon);
        }

        public override void OnClick()
        {
            _playerHealth.UpdateMaxArmor(-mainWeapon.GetComponent<Helmet>().armorAmount);
            base.OnClick();
            playerEquipmentSystem.RemoveHelmet();
        }
    }
}