using UnityEngine;

namespace _Scripts.MissionsSystems
{
	public class TpClicker : MonoBehaviour
	{
		public GameObject tpLocation;
		private GameObject _player;
		private CharacterController _characterController;

		private void Awake()
		{
			_player = GameObject.FindGameObjectWithTag("Player");
			_characterController = _player.GetComponent<CharacterController>();
		}

		public void OnClick()
		{
			_characterController.enabled = false;
			_player.transform.position = tpLocation.transform.position;
			_characterController.enabled = true;
		}
	}
}