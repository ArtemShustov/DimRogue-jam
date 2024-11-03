using Game.PointBasedMovement;
using System;
using UnityEngine;

namespace Game.Characters {
	public class Character: MonoBehaviour, ITarget {
		[SerializeField] private PointMovement _movement;
		[SerializeField] private Attacker _attack;
		[SerializeField] private Attackable _attackable;

		public PointMovement Movement => _movement;
		public Attacker Attack => _attack;

		public Point Root => _movement.Current;

		public virtual bool TryAct(Point point, Action onCompleteCallback = null) {
			if (point == _movement.Current) {
				return false;
			}
			if (point.IsOccupied) {
				if (point.Entity.TryGetComponent<IAttackable>(out var attackable) && _attack.CanAttack(attackable)) {
					_attack.Attack(attackable, onCompleteCallback);
					return true;
				}
				if (point.Entity.TryGetComponent<IUsable>(out var usable)) {
					onCompleteCallback?.Invoke();
					return true;
				}
			} else {
				if (_movement.CanMoveTo(point)) {
					_movement.MoveTo(point, onCompleteCallback);
					return true;
				}
			}
			return false;
		}

		public void TakeDamage(int points) {
			_attackable.TakeDamage(points);
		}
	}
}