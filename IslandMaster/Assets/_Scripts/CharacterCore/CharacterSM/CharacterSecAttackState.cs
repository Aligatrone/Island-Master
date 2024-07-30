using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
    public class CharacterSecAttackState : CharacterBaseState
    {
        private static readonly int SecAttack = Animator.StringToHash("SecAttack");
        private static readonly int Move = Animator.StringToHash("Move");
        
        private float _timePassed;
        private float _clipLength;
        private float _clipSpeed;

        public CharacterSecAttackState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            Context.Animator.SetTrigger(SecAttack);
            _timePassed = 0f;
        }

        public override void UpdateState()
        {
            _timePassed += Time.deltaTime;
            _clipLength = Context.Animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            _clipSpeed = Context.Animator.GetCurrentAnimatorStateInfo(1).speed;
            
            CheckSwitchStates();
        }

        public override void PhysicsUpdate()
        {
            
        }

        protected override void ExitState()
        {
        }
        
        protected override void CheckSwitchStates()
        {
            base.CheckSwitchStates();
            
            if (_timePassed >= _clipLength / _clipSpeed)
            {
                Context.Animator.SetTrigger(Move);
                SwitchState(Factory.Combat());
            }
        }
    }
}