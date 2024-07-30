using System;
using UnityEngine;

namespace _Scripts.InventorySystem.ConcreteItems
{
    public class Armor : InventoryItemBase
    {
        public static Action<GameObject> ArmorEquiped;
        public int armorAmount;
	
        public override void OnUse()
        {
            OnRemove();
            ArmorEquiped?.Invoke(gameObject);
        }
    }
}