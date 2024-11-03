namespace Game.Characters.Hero {
	public class NonAttackable: Attackable {
		public override void TakeDamage(int points) {
			return;
		}
	}
}