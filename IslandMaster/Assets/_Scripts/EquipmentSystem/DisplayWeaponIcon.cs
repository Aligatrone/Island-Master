using _Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.EquipmentSystem
{
	public class DisplayWeaponIcon : MonoBehaviour
	{
		private Image weaponIcon; 
	
		private void Awake()
		{
			weaponIcon = GetComponent<Image>();
		}

		public void Display(GameObject o)
		{
			weaponIcon.sprite = o.GetComponent<IInventoryItem>().Image;
			weaponIcon.enabled = true;
		}

		public void HideDisplay()
		{
			weaponIcon.enabled = false;
		}
	}
}