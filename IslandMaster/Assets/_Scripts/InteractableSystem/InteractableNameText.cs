using TMPro;
using UnityEngine;

namespace _Scripts.InteractableSystem
{
    public class InteractableNameText : MonoBehaviour
    {
        TextMeshProUGUI text;
 
        Transform cameraTransform;
        void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            cameraTransform = Camera.main.transform;
            HideText();
        }
        public void ShowText(Interactable interactable)
        {
            if (interactable is PickUpItem)
            {
                text.text = interactable.interactableName+"\n [F] Pick Up";
            }
            else if(interactable is InteractableLoot)
            {
                text.text = interactable.interactableName + "\n [F] Loot";
            }
            else if(interactable is InteractableNpc)
            {
                text.text = interactable.interactableName + "\n [F] Speak";
            }
            else if(interactable is InteractableChest)
            {
                text.text = interactable.interactableName + "\n [F] Open";
            }
            else
            {
                text.text = interactable.interactableName;
            }
 
        }
 
        public void HideText()
        {
            text.text = "";
        }
 
        public void SetInteractableNamePosition(Interactable interactable)
        {
            if (interactable.TryGetComponent(out BoxCollider boxCollider))
            {
                transform.position = interactable.transform.position + Vector3.up * boxCollider.bounds.size.y;
                transform.LookAt(2 * transform.position - cameraTransform.position);
            }
            else if(interactable.TryGetComponent(out CapsuleCollider capsCollider))
            {
                transform.position = interactable.transform.position + Vector3.up * capsCollider.height;
                transform.LookAt(2 * transform.position - cameraTransform.position);
            }
            else
            {
                print("Error, no collider found!");
            }
      
 
        }
    }
}