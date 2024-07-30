using _Scripts.Health;
using _Scripts.InteractableSystem;
using _Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.CharacterCore.CharacterSM
{
	public class CharacterStateMachine : MonoBehaviour
	{
		public float playerSpeed = 5.0f;
		public float sprintSpeed = 7.0f;
		public float crouchSpeed = 2.0f;
		public float gravityMultiplier = 2;
		public float rotationSpeed = 5f;
		public float crouchColliderHeight = 1.35f;
	
		[Range(0, 1)]
		public float speedDampTime = 0.1f;
		[Range(0, 1)]
		public float velocityDampTime = 0.9f;
		[Range(0, 1)]
		public float rotationDampTime = 0.2f;
	
		[HideInInspector]
		public float gravityValue = -9.81f;
		[HideInInspector]
		public float normalColliderHeight;
		[HideInInspector]
		public Vector3 playerVelocity;
	
		private CharacterStateFactory _factory;
		private CharacterBaseState _currentState;

		private PlayerInput _playerInput;
	
		public InputAction moveAction;
		public InputAction runAction;
		public InputAction crouchAction;
		public InputAction drawWeaponAction;
		public InputAction attackAction;
		public InputAction reviveAction;
		public InputAction dodgeAction;
		public InputAction secundaryAttackAction;
		public InputAction mapAction;

		[SerializeField] private GameObject playerMap;

		public bool playerDied;

		public CharacterBaseState CurrentState
		{
			set => _currentState = value;
			get => _currentState;
		}

		public CharacterController CharacterController { get; private set; }
		public Animator Animator { get; private set; }
		public Transform CameraTransform { get; private set; }
		public PlayerHealth playerHealthSystem;
		public CharacterCore.EquipmentSystem playerEquipmentSystem;
		
		private static readonly int Damage = Animator.StringToHash("Damage");
		private static readonly int Up = Animator.StringToHash("PickUp");

		private void Awake()
		{
			CharacterController = GetComponent<CharacterController>();
			Animator = GetComponent<Animator>();
			_playerInput = GetComponent<PlayerInput>();
			playerHealthSystem = GetComponent<PlayerHealth>();
			playerEquipmentSystem = GetComponent<CharacterCore.EquipmentSystem>();
			if(Camera.main != null) CameraTransform = Camera.main.transform;
		
			_factory = new CharacterStateFactory(this);
			_currentState = _factory.Standing();
			_currentState.EnterState();
		
			normalColliderHeight = CharacterController.height;
			gravityValue *= gravityMultiplier;

			moveAction = _playerInput.actions["Move"];
			runAction = _playerInput.actions["Run"];
			drawWeaponAction = _playerInput.actions["DrawWeapon"];
			attackAction = _playerInput.actions["Attack"];
			crouchAction = _playerInput.actions["Crouch"];
			reviveAction = _playerInput.actions["Revive"];
			dodgeAction = _playerInput.actions["Dodge"];
			secundaryAttackAction = _playerInput.actions["SecondAttack"];
			mapAction = _playerInput.actions["Map"];
		}

		private void OnEnable()
		{
			playerHealthSystem.OnEntityDamaged += TakeDamage;
			playerHealthSystem.OnEntityDeath += OnDeath;
			PickUpItem.AddItemToInventory += PickUp;
		}

		private void OnDisable()
		{
			playerHealthSystem.OnEntityDamaged -= TakeDamage;
			playerHealthSystem.OnEntityDeath -= OnDeath;
			PickUpItem.AddItemToInventory -= PickUp;
		}

		private void PickUp(object sender, InventoryEventArgs e)
		{
			Animator.SetTrigger(Up);
		}

		private void TakeDamage()
		{
			Animator.SetTrigger(Damage);
		}

		private void OnDeath()
		{
			playerDied = true;
		}

		private void Update()
		{
			if(mapAction.triggered)
			{
				playerMap.SetActive(!playerMap.activeSelf);
			}
			
			_currentState.UpdateState();
		}

		private void FixedUpdate()
		{
			_currentState.PhysicsUpdate();
		}
	}
}