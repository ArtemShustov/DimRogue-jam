using Game.Characters.Ghost;
using Game.PointBasedMovement;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameFlow {
	public class AITurn: MonoBehaviour, ITurn {
		[SerializeField] private GhostPool _pool;
		[SerializeField] private TurnMachine _machine;
		private Queue<GhostCharacter> _ghostQueue;

		public void StepFor(GhostCharacter ghost) {
			var colliders = Physics.OverlapSphere(ghost.transform.position, ghost.Movement.MaxDistance);
			var points = new List<Point>();
			foreach (var collider in colliders) {
				if (collider.TryGetComponent<Point>(out var point) && point != ghost.Movement.Current) {
					points.Add(point);
				}
			}
			if (points.Count == 0) {
				OnGhostActionCompleted();
				return;
			}
			var target = points[Random.Range(0, points.Count)];

			ghost.SetRandomState();
			var result = ghost.TryAct(target, OnGhostActionCompleted);
			if (!result) {
				OnGhostActionCompleted();
			}
		}

		private void OnGhostActionCompleted() {
			if (_ghostQueue.Count > 0) {
				StepFor(_ghostQueue.Dequeue());
			} else {
				_machine.NextTurn();
			}
		}

		public void OnEnter() {
			IReadOnlyList<GhostCharacter> ghosts = _pool.Spawned;
			_ghostQueue = new Queue<GhostCharacter>(ghosts);

			if (_ghostQueue.Count == 0) {
				_machine.NextTurn();
			} else {
				StepFor(_ghostQueue.Dequeue());
			}
		}

		public void OnExit() { }
	}
}
