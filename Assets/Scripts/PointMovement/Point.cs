using UnityEngine;

namespace Game.PointBasedMovement {
	[RequireComponent(typeof(SphereCollider))]
	public class Point: MonoBehaviour, ITarget {
		private PointMovement _entity;

		public PointMovement Entity => _entity;
		public bool IsOccupied => _entity != null;
		public Point Root => this;

		private void OnDrawGizmos() {
			Gizmos.DrawWireSphere(transform.position, 0.5f);
			Gizmos.DrawLine(transform.position, transform.position + transform.up);
		}

		public virtual void OnEnter(PointMovement gameObject) { 
			if (_entity != null) {
				throw new System.InvalidOperationException("Point is occupied");
			}
			_entity = gameObject;
		}
		public virtual void OnExit(PointMovement gameObject) {
			if (_entity == null) {
				return;
			}
			_entity = null;
		}
	}
}