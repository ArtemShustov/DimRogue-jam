using Game.PointBasedMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Ghost {
	public class GhostPool: MonoBehaviour {
		[SerializeField] private float _nearDist = 4;
		[SerializeField] private GhostCharacter _prefab;

		public event Action AllDied;

		private List<GhostCharacter> _spawned = new List<GhostCharacter>();
		public IReadOnlyList<GhostCharacter> Spawned => _spawned;

		public GhostCharacter SpawnNear(Transform transform) {
			var colliders = Physics.OverlapSphere(transform.position, _nearDist)
				.Where((collider) => collider.TryGetComponent<Point>(out var point) && !point.IsOccupied)
				.Select((collider) => collider.GetComponent<Point>())
				.ToArray();
			if (colliders.Length == 0 ) {
				return null;
			}
			var point = colliders[UnityEngine.Random.Range(0, colliders.Length)];
			var ghost = Spawn();
			ghost.Movement.AttachTo(point);
			return ghost;
		}
		public GhostCharacter SpawnAt(Point point) {
			var ghost = Spawn();
			ghost.Movement.AttachTo(point);
			return ghost;
		}
		public GhostCharacter Spawn() {
			var ghost = Instantiate(_prefab);
			ghost.SetPool(this);
			_spawned.Add(ghost);
			return ghost;
		}

		public void OnDie(GhostCharacter ghost) {
			_spawned.Remove(ghost);
			if (_spawned.Count == 0 ) {
				AllDied?.Invoke();
			}
		}
	}
}