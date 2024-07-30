using _Scripts.InventorySystem;
using UnityEngine;

namespace _Scripts.EquipmentSystem
{
	public class EquipmentClick : MonoBehaviour
	{
		public GameObject mainWeapon;
		[SerializeField] protected Inventory playerInventory;
		[SerializeField] protected CharacterCore.EquipmentSystem playerEquipmentSystem;
		[SerializeField] protected DisplayWeaponIcon mainWeaponDisplay;

		protected virtual void OnWeaponEquiped(GameObject weapon)
		{
			if(mainWeapon != null)
				playerInventory.AddItem(mainWeapon.GetComponent<IInventoryItem>()); 
		
			mainWeapon = weapon;
		
			mainWeaponDisplay.Display(mainWeapon);
		}

		public virtual void OnClick()
		{
			if(playerInventory.AddItem(mainWeapon.GetComponent<IInventoryItem>()))
			{
				mainWeapon = null;
			}
		
			mainWeaponDisplay.HideDisplay();
		}
	}
}