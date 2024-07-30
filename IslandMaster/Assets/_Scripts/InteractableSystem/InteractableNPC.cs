using UnityEngine;

namespace _Scripts.InteractableSystem
{
    public class InteractableNpc : Interactable
    {
        [SerializeField] private GameObject hudToOpen;

        protected override void Interaction()
        {
            base.Interaction();
            hudToOpen.SetActive(true);
        }
    }
}