using UnityEngine;

namespace _Scripts.InteractableSystem
{
    public class Interactable : MonoBehaviour
    {
        [Header("Interaction Data")]
        public string interactableName="";
        public float interactionDistance = 2;
        public bool isInteractable = true;
 
        InteractableNameText interactableNameText;
        GameObject interactableNameCanvas;
 
        public virtual void Start()
        {
            interactableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");
            interactableNameText = interactableNameCanvas.GetComponentInChildren<InteractableNameText>();
        }
 
        public void TargetOn()
        {
            interactableNameText.ShowText(this);
            interactableNameText.SetInteractableNamePosition(this);
        }
 
        public void TargetOff()
        {
            if(interactableNameText == null) return;
            
            interactableNameText.HideText();
        }
 
        public void Interact()
        {
            if(isInteractable) Interaction();
        }
 
        protected virtual void Interaction()
        {
        }
 
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,interactionDistance);
        }
        private void OnDestroy()
        {
            TargetOff();
        }
    }
}