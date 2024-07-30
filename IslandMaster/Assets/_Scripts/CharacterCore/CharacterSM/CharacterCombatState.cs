using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
    public class CharacterCombatState : CharacterBaseState
    {
        private float _gravityValue;
        private Vector3 _currentVelocity;
        private bool _grounded;
        private float _playerSpeed;
        private float _playerRunSpeed;

        private Vector3 _gravityVelocity;
        private Vector3 _velocity;
        private Vector2 _input;
        private Vector3 _cVelocity;
        
        private static readonly int DrawWeapon = Animator.StringToHash("DrawWeapon");
        private static readonly int SheathWeapon = Animator.StringToHash("SheathWeapon");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public CharacterCombatState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {}

        public override void EnterState()
        {
            Context.Animator.SetTrigger(DrawWeapon);
            
            _input = Vector2.zero;
            _velocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
            _gravityVelocity.y = 0;
 
            _playerSpeed = Context.playerSpeed;
            _playerRunSpeed = Context.sprintSpeed;
			
            _grounded = Context.CharacterController.isGrounded;
            _gravityValue = Context.gravityValue;
        }

        public override void UpdateState()
        {
            _input = Context.moveAction.ReadValue<Vector2>();
            _velocity = new Vector3(_input.x, 0, _input.y);
 
            _velocity = _velocity.x * Context.CameraTransform.right.normalized + _velocity.z * Context.CameraTransform.forward.normalized;
            _velocity.y = 0f;

            float speedValue = _input.magnitude;
            if(Context.runAction.IsPressed() && speedValue != 0f) speedValue += 0.5f;
			
            Context.Animator.SetFloat(Speed, speedValue, Context.speedDampTime, Time.deltaTime);
            
            CheckSwitchStates();
        }

        public override void PhysicsUpdate()
        {
            _gravityVelocity.y += _gravityValue * Time.deltaTime;
            _grounded = Context.CharacterController.isGrounded;
 
            if (_grounded && _gravityVelocity.y < 0)
            {
                _gravityVelocity.y = 0f;
            }
       
            _currentVelocity = Vector3.SmoothDamp(_currentVelocity, _velocity, ref _cVelocity, Context.velocityDampTime);

            float currentSpeed = Context.runAction.IsPressed() ? _playerRunSpeed : _playerSpeed;
            Context.CharacterController.Move(_currentVelocity * (Time.deltaTime * currentSpeed) + _gravityVelocity * Time.deltaTime);
  
            if (_velocity.sqrMagnitude>0)
            {
                Context.CharacterController.transform.rotation = Quaternion.Slerp(Context.CharacterController.transform.rotation, Quaternion.LookRotation(_velocity), Context.rotationDampTime);
            }
        }

        protected override void ExitState()
        {
            _gravityVelocity.y = 0f;
            Context.playerVelocity = new Vector3(_input.x, 0, _input.y);
 
            if (_velocity.sqrMagnitude > 0)
            {
                Context.transform.rotation = Quaternion.LookRotation(_velocity);
            }
        }

        protected override void CheckSwitchStates()
        {
            base.CheckSwitchStates();
            
            if(Context.drawWeaponAction.triggered)
            {
                Context.Animator.SetTrigger(SheathWeapon);
                SwitchState(Factory.Standing());
            }

            if(Context.attackAction.triggered)
            {
                Context.Animator.SetTrigger(Attack);
                SwitchState(Factory.Attacking());
            }

            if(Context.secundaryAttackAction.triggered)
            {
                SwitchState(Factory.SecundaryAttack());
            }

            if(Context.dodgeAction.triggered)
            {
                SwitchState(Factory.Dodge());
            }
        }
    }
}