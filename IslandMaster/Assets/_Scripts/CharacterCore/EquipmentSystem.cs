using UnityEngine;

namespace _Scripts.CharacterCore
{
	public class EquipmentSystem : MonoBehaviour
	{
		[SerializeField] private GameObject weaponHolder;
		public GameObject weapon;
		[SerializeField] private GameObject weaponSheath;

		public GameObject armor;
		[SerializeField] private GameObject armorSlot;

		public GameObject helmet;
		[SerializeField] private GameObject helmetSlot;
		
		public void AddWeapon(GameObject weaponEquiped)
		{
			weapon = weaponEquiped.gameObject;
			weapon.SetActive(true);
			weapon.transform.parent = weaponSheath.transform;
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localRotation = Quaternion.identity;
		}

		public void AddArmor(GameObject armorEq)
		{
			armor = armorEq.gameObject;
			armor.SetActive(true);
			armor.transform.parent = armorSlot.transform;
			armor.transform.localPosition = Vector3.zero;
			armor.transform.localRotation = Quaternion.identity;
		}
		
		public void AddHelmet(GameObject helmetEq)
		{
			helmet = helmetEq.gameObject;
			helmet.SetActive(true);
			helmet.transform.parent = helmetSlot.transform;
			helmet.transform.localPosition = Vector3.zero;
			helmet.transform.localRotation = Quaternion.identity;
		}
		
		public void RemoveHelmet()
		{
			helmet.transform.parent = null;
			helmet = null;
		}

		public void RemoveArmor()
		{
			armor.transform.parent = null;
			armor = null;
		}

		public void RemoveWeapon()
		{
			weapon.transform.parent = null;
			weapon = null;
		}
 
		public void DrawWeapon()
		{
			if(weapon == null) return;

			weapon.transform.parent = weaponHolder.transform;
		}
 
		public void SheathWeapon()
		{
			if(weapon == null) return;
			
			weapon.transform.parent = weaponSheath.transform;
		}
 
		public void StartDealDamage()
		{
			if(weapon == null) return;
			
			weapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
		}
		public void EndDealDamage()
		{
			if(weapon == null) return;
			
			weapon.GetComponentInChildren<DamageDealer>().EndDealDamage();
		}
	}
}