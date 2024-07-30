using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
    public class CharacterAttackState : CharacterBaseState
    {
        private float _timePassed;
        private float _clipLength;
        private float _clipSpeed;
        private bool _attack;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Move = Animator.StringToHash("Move");

        public CharacterAttackState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {}

        public override void EnterState()
        {
            _attack = false;
            Context.Animator.applyRootMotion = true;
            _timePassed = 0f;
            Context.Animator.SetTrigger(Attack);
            Context.Animator.SetFloat(Speed, 0f);
        }

        public override void UpdateState()
        {
            if(Context.attackAction.triggered)
                _attack = true;
            
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
            Context.Animator.applyRootMotion = false;
        }

        protected override void CheckSwitchStates()
        {
            base.CheckSwitchStates();
            
            if (_timePassed >= _clipLength / _clipSpeed && _attack)
            {
                SwitchState(Factory.Attacking());
                return;
            }
            
            if (_timePassed >= _clipLength / _clipSpeed)
            {
                Context.Animator.SetTrigger(Move);
                SwitchState(Factory.Combat());
            }
        }
    }
}