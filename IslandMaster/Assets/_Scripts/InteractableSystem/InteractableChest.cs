using UnityEngine;

namespace _Scripts.InteractableSystem
{
    public class InteractableChest : Interactable
    {
        public bool isOpen;
        [SerializeField] private GameObject chestPanel;
 
        public override void Start()
        {
            base.Start();
            isOpen = false;
        }
 
        protected override void Interaction()
        {
            base.Interaction();
            chestPanel.SetActive(true);
        }
    }
}
