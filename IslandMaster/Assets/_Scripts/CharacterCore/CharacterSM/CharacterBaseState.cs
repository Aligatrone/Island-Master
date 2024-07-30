namespace _Scripts.CharacterCore.CharacterSM
{
	public abstract class CharacterBaseState
	{
		protected readonly CharacterStateMachine Context;
		protected readonly CharacterStateFactory Factory;

		protected CharacterBaseState(CharacterStateMachine currentContext, CharacterStateFactory playerStateFactory)
		{
			Context = currentContext;
			Factory = playerStateFactory;
		}
	
		public abstract void EnterState();
		public abstract void UpdateState();
		public abstract void PhysicsUpdate();
		protected abstract void ExitState();

		protected virtual void CheckSwitchStates()
		{
			if(Context.playerDied)
				SwitchState(Factory.Death());
		}
	
		protected void SwitchState(CharacterBaseState newState)
		{
			ExitState();
			
			newState.EnterState();

			Context.CurrentState = newState;
		}
	}
}