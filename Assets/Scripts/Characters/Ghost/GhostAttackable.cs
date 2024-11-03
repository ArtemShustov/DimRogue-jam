using System;
using UnityEngine;

namespace Game.Characters.Ghost {
	public class GhostAttackable: Attackable {
		[SerializeField] private GhostCharacter _ghost;
		[SerializeField] private ParticleEffect _dieFX;
		private GhostCharacter.State _state;

		public event Action Died;

		public override void TakeDamage(int points) {
			switch (_state) {
				case GhostCharacter.State.Neutral:
					_dieFX.PlayEffect();
					Died?.Invoke();
					Destroy(gameObject);
					break;
				case GhostCharacter.State.Agressive:
					_ghost.SpawnClone();
					break;
				default:
					break;
			}
		}

		public void SetState(GhostCharacter.State state) {
			_state = state;
		}
	}
}