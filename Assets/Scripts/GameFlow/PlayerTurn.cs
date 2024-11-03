using Game.Characters;
using Game.PointBasedMovement;
using UnityEngine;

namespace Game.GameFlow {
	public class PlayerTurn: MonoBehaviour, ITurn {
		[SerializeField] private TargetSelector _pointSelector;
		[SerializeField] private PointHighlighter _pointHighlighter;
		[SerializeField] private Character _player;
		[SerializeField] private TurnMachine _machine;

		private void OnPointSelected(Point point) {
			if (point == _player.Movement.Current) {
				return;
			}
			var result = _player.TryAct(point, () => _machine.NextTurn());
			if (result) {
				_pointSelector.Selected -= OnPointSelected;
			}
		}

		public void OnEnter() {
			_pointSelector.Selected += OnPointSelected;
			_pointHighlighter.enabled = true;
		}
		public void OnExit() {
			_pointSelector.Selected -= OnPointSelected;
			_pointHighlighter.enabled = false;
		}
	}
}