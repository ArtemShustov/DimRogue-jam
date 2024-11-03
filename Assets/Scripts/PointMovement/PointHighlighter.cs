using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.PointBasedMovement {
	public class PointHighlighter: MonoBehaviour {
		[SerializeField] private float _radius;
		[SerializeField] private LayerMask _mask;
		[SerializeField] private Camera _camera;
		[SerializeField] private PointMovement _entity;

		private PointView _highlighted;
		private List<PointView> _showed = new List<PointView>();

		private void Update() {
			var ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, maxDistance: 50, layerMask: _mask)) {
				UpdateHighlight(hit);
				UpdateTransparency(hit.point);
			} else {
				ClearHighlight();
				ClearTransparency();
			}
		}

		private void UpdateHighlight(RaycastHit hit) {
			if (hit.transform.TryGetComponent<PointView>(out var point)) {
				if (point == _highlighted) {
					return;
				}
				ClearHighlight();
				_highlighted = point;
				bool canMove = _entity.CanMoveTo(point.Point);
				var highlight = canMove ? PointView.HighlightType.Positive : PointView.HighlightType.Negative;
				_highlighted.SetHighlight(highlight);
			} else {
				ClearHighlight();
			}
		}
		private void ClearHighlight() {
			if (_highlighted == null) {
				return;
			}
			_highlighted.SetHighlight(PointView.HighlightType.None);
			_highlighted = null;
		}
		private void UpdateTransparency(Vector3 root) {
			var showedNow = new List<PointView>();
			var colliders = Physics.OverlapSphere(root, _radius, _mask);
			foreach (var collider in colliders) {
				if (collider.TryGetComponent<PointView>(out var point)) {
					var alpha = Mathf.Clamp01(1 - Vector3.Distance(root, point.transform.position) / _radius);
					point.SetTransparency(alpha);
					showedNow.Add(point);
				}
			}
			foreach (var point in _showed.Except(showedNow)) {
				point.SetTransparency(0);
			}
			_showed = showedNow;
		}
		private void ClearTransparency() {
			if (_showed.Count == 0) {
				return;
			}
			foreach (var point in _showed) {
				point.SetTransparency(0);
			}
			_showed.Clear();
		}

		private void OnDisable() {
			ClearHighlight();
			ClearTransparency();
		}
	}
}