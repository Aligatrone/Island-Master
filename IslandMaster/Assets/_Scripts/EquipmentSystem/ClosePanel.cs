using UnityEngine;

namespace _Scripts.EquipmentSystem
{
    public class ClosePanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelToClose;

        public void Close()
        {
            panelToClose.SetActive(false);
        }
    }
}