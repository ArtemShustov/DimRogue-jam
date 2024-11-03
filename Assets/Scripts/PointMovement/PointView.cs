using UnityEngine;

namespace Game.PointBasedMovement {
	[RequireComponent(typeof(Point))]
	public class PointView: MonoBehaviour {
		[SerializeField] private MeshRenderer _renderer;
		[SerializeField, HideInInspector] private Point _point;

		public Point Point => _point;

		private void Awake() {
			_point = GetComponent<Point>();
		}
		private void Start() {
			SetHighlight(HighlightType.None);
			SetTransparency(0);
		}

		public void SetTransparency(float value) {
			var color = _renderer.material.color;
			color.a = Mathf.Clamp01(value);
			_renderer.material.color = color;
		}
		public void SetHighlight(HighlightType value) {
			var alpha = _renderer.material.color.a;
			var newColor = value switch {
				HighlightType.Positive => Color.green,
				HighlightType.Negative => Color.red,
				_ => Color.white,
			};
			newColor.a = alpha;
			_renderer.material.color = newColor;
		}

		public enum HighlightType {
			None,
			Positive,
			Negative
		}
	}
}