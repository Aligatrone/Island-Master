using _Scripts.InventorySystem.ConcreteItems;
using UnityEngine;

namespace _Scripts.EquipmentSystem
{
	public class MainWeaponClick : EquipmentClick
	{
		private void OnEnable()
		{
			Weapon.WeaponEquiped += OnWeaponEquiped;
		}

		private void OnDisable()
		{
			Weapon.WeaponEquiped += OnWeaponEquiped;
		}
	
		protected override void OnWeaponEquiped(GameObject weapon)
		{
			base.OnWeaponEquiped(weapon);
			playerEquipmentSystem.AddWeapon(mainWeapon);
		}

		public override void OnClick()
		{
			base.OnClick();
			playerEquipmentSystem.RemoveWeapon();
		}
	}
}