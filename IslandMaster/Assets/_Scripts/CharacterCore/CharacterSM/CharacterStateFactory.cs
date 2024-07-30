namespace _Scripts.CharacterCore.CharacterSM
{
	public class CharacterStateFactory
	{
		private readonly CharacterStateMachine _context;

		public CharacterStateFactory(CharacterStateMachine currentContext)
		{
			_context = currentContext;
		}

		public CharacterBaseState Standing()
		{
			return new CharacterStandingState(_context, this);
		}

		public CharacterBaseState Combat()
		{
			return new CharacterCombatState(_context, this);
		}
		
		public CharacterBaseState Attacking()
		{
			return new CharacterAttackState(_context, this);
		}

		public CharacterBaseState Crouching()
		{
			return new CharacterCrouchState(_context, this);
		}

		public CharacterBaseState Death()
		{
			return new CharacterDeathState(_context, this);
		}

		public CharacterBaseState Dodge()
		{
			return new CharacterDodgeState(_context, this);
		}

		public CharacterBaseState SecundaryAttack()
		{
			return new CharacterSecAttackState(_context, this);
		}
	}
}