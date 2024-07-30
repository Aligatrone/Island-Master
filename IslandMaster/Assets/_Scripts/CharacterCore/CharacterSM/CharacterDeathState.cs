using UnityEngine;

namespace _Scripts.CharacterCore.CharacterSM
{
    public class CharacterDeathState : CharacterBaseState
    {
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Revive = Animator.StringToHash("Revive");

        public CharacterDeathState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            Context.Animator.SetTrigger(Dead);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void PhysicsUpdate()
        {
            
        }

        protected override void ExitState()
        {
            Context.Animator.SetTrigger(Revive);
        }

        protected override void CheckSwitchStates()
        {
            if(Context.reviveAction.triggered)
            {
                Context.playerHealthSystem.Revive();
                Context.playerDied = false;
                SwitchState(Factory.Standing());
            }
        }
    }
}