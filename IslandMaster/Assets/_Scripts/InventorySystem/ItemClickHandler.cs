using UnityEngine;

namespace _Scripts.InventorySystem
{
	public class ItemClickHandler : MonoBehaviour
	{
		public IInventoryItem item;
		[SerializeField] public bool isChestOpen;
		
		public void OnItemClicked()
		{
			if(isChestOpen)
				item?.OnMove();
			item?.OnUse();
		}
	}
}