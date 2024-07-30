using System;
using UnityEngine;

namespace _Scripts.InventorySystem.ConcreteItems
{
    public class Helmet : InventoryItemBase
    {
        public static Action<GameObject> HelmetEquiped;
        public int armorAmount;
	
        public override void OnUse()
        {
            OnRemove();
            HelmetEquiped?.Invoke(gameObject);
        }
    }
}