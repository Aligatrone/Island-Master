using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
	public class CharacterCrouchState : CharacterBaseState
	{
		private float _playerSpeed;
		private bool _belowCeiling;
		private bool _crouchHeld;

		private bool _grounded;
		private float _gravityValue;
		private Vector3 _currentVelocity;
		private Vector3 _gravityVelocity;
		private Vector3 _velocity;
		private Vector2 _input;
		private Vector3 _cVelocity;
		
		private static readonly int Crouch = Animator.StringToHash("Crouch");
		private static readonly int Move = Animator.StringToHash("Move");
		private static readonly int Speed = Animator.StringToHash("Speed");

		public CharacterCrouchState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
		{
		}

		public override void EnterState()
		{
			Context.Animator.SetTrigger(Crouch);
			_belowCeiling = false;
			_crouchHeld = false;
			_gravityVelocity.y = 0;
 
			_playerSpeed = Context.crouchSpeed;
			Context.CharacterController.height = Context.crouchColliderHeight;
			Context.CharacterController.center = new Vector3(0f, Context.crouchColliderHeight / 2f, 0f);
			_grounded = Context.CharacterController.isGrounded;
			_gravityValue = Context.gravityValue;
		}
		
		public override void UpdateState()
		{
			if (Context.crouchAction.triggered && !_belowCeiling)
			{
				_crouchHeld = true;
			}
			
			_input = Context.moveAction.ReadValue<Vector2>();
			_velocity = new Vector3(_input.x, 0, _input.y);
 
			_velocity = _velocity.x * Context.CameraTransform.right.normalized + _velocity.z * Context.CameraTransform.forward.normalized;
			_velocity.y = 0f;
			
			Context.Animator.SetFloat(Speed, _input.magnitude, Context.speedDampTime, Time.deltaTime);
			
			CheckSwitchStates();
		}

		public override void PhysicsUpdate()
		{
			_belowCeiling = CheckCollisionOverlap(Context.transform.position + Vector3.up * Context.normalColliderHeight);
			_gravityVelocity.y += _gravityValue * Time.deltaTime;
			_grounded = Context.CharacterController.isGrounded;
			if (_grounded && _gravityVelocity.y < 0)
			{
				_gravityVelocity.y = 0f;
			}
			_currentVelocity = Vector3.Lerp(_currentVelocity, _velocity, Context.velocityDampTime);
 
			Context.CharacterController.Move(_currentVelocity * (Time.deltaTime * _playerSpeed) + _gravityVelocity * Time.deltaTime);
 
			if (_velocity.magnitude > 0)
			{
				Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, Quaternion.LookRotation(_velocity), Context.rotationDampTime);
			}
		}

		protected override void ExitState()
		{
			Context.CharacterController.height = Context.normalColliderHeight;
			Context.CharacterController.center = new Vector3(0f, Context.normalColliderHeight / 2f, 0f);
			_gravityVelocity.y = 0f;
			Context.playerVelocity = new Vector3(_input.x, 0, _input.y);
			Context.Animator.SetTrigger(Move);
		}

		protected override void CheckSwitchStates()
		{
			base.CheckSwitchStates();
			
			if(_crouchHeld)
			{
				SwitchState(Factory.Standing());
			}
		}
		
		private bool CheckCollisionOverlap(Vector3 targetPosition)
		{
			int layerMask = 1 << 8;
			layerMask = ~layerMask;

			Vector3 direction = targetPosition - Context.transform.position;
			return Physics.Raycast(Context.transform.position, direction, out var hit, Context.normalColliderHeight, layerMask);
		}
	}
}