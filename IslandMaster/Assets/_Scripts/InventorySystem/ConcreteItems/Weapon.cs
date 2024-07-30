using System;
using UnityEngine;

namespace _Scripts.InventorySystem.ConcreteItems
{
	public class Weapon : InventoryItemBase
	{
		public static Action<GameObject> WeaponEquiped;
	
		public override void OnUse()
		{
			OnRemove();
			WeaponEquiped?.Invoke(gameObject);
		}
	}
}