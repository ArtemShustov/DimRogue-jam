using Game.InputHandling;
using Game.PointBasedMovement;
using System;
using UnityEngine;

namespace Game {
	public class TargetSelector: MonoBehaviour {
		[SerializeField] private LayerMask _mask;
		[SerializeField] private Camera _camera;
		[SerializeField] private ClickWithoutMovement _input;
		[SerializeField] private TargetSelectorFX _fx;

		public event Action<Point> Selected;

		private bool TrySelect(out Point point) {
			var ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, maxDistance: 50, layerMask: _mask)) {
				if (hit.collider.TryGetComponent<ITarget>(out var target)) {
					point = target.Root;
					_fx.PlaySelected();
					return true;
				}
			}
			point = null;
			_fx.PlayRejected();
			return false;
		}

		private void OnClick() {
			if (TrySelect(out var target)) {
				Selected?.Invoke(target);
			}
		}

		private void OnEnable() {
			_input.Performed += OnClick;
		}
		private void OnDisable() {
			_input.Performed -= OnClick;
		}
	}
}