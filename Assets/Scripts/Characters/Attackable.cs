using UnityEngine;

namespace Game.Characters {
	public abstract class Attackable: MonoBehaviour, IAttackable {
		public abstract void TakeDamage(int points);
	}
}