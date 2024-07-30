using System;
using UnityEngine;

namespace _Scripts.InventorySystem.ConcreteItems
{
    public class HealingPotion : InventoryItemBase
    {
        public static Action<int> HealPlayer;
        
        [SerializeField]
        public int healingAmount;

        public override string Name => "HealingPotion";

        public override void OnUse()
        {
            HealPlayer?.Invoke(healingAmount);
            OnRemove();
        }
    }
}