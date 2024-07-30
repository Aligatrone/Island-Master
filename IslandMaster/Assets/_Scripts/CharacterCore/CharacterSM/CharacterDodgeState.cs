using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
    public class CharacterDodgeState : CharacterBaseState
    {
        private float _timePassed;
        private float _clipLength;
        private float _clipSpeed;
        private static readonly int Dodge = Animator.StringToHash("Dodge");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Move = Animator.StringToHash("Move");

        public CharacterDodgeState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            Context.Animator.applyRootMotion = true;
            _timePassed = 0f;
            Context.Animator.SetTrigger(Dodge);
            Context.Animator.SetFloat(Speed, 0f);
            Context.playerHealthSystem.isImmune = true;
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
            Context.Animator.applyRootMotion = false;
            Context.playerHealthSystem.isImmune = false;
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