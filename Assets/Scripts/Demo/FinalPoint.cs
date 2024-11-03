using Game.PointBasedMovement;
using System;

namespace Game.Demo {
	public class FinalPoint: Point {
		public event Action Reached;

		public override void OnEnter(PointMovement gameObject) {
			Reached?.Invoke();
			base.OnEnter(gameObject);
		}
	}
}