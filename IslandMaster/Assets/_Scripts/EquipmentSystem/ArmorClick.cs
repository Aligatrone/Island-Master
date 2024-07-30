using _Scripts.Health;
using _Scripts.InventorySystem.ConcreteItems;
using UnityEngine;

namespace _Scripts.EquipmentSystem
{
    public class ArmorClick : EquipmentClick
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        private void OnEnable()
        {
            Armor.ArmorEquiped += OnWeaponEquiped;
        }

        private void OnDisable()
        {
            Armor.ArmorEquiped -= OnWeaponEquiped;
        }
        
        protected override void OnWeaponEquiped(GameObject weapon)
        {
            if(mainWeapon != null)
                _playerHealth.UpdateMaxArmor(-mainWeapon.GetComponent<Armor>().armorAmount);
            
            base.OnWeaponEquiped(weapon);
            
            _playerHealth.UpdateMaxArmor(mainWeapon.GetComponent<Armor>().armorAmount);
            playerEquipmentSystem.AddArmor(mainWeapon);
        }

        public override void OnClick()
        {
            _playerHealth.UpdateMaxArmor(-mainWeapon.GetComponent<Armor>().armorAmount);
            base.OnClick();
            playerEquipmentSystem.RemoveArmor();
        }
    }
}