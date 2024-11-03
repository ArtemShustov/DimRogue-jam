using DG.Tweening;
using System;
using UnityEngine;

namespace Game.PointBasedMovement {
	public class PointMovement: MonoBehaviour {
		[SerializeField] private float _maxDisance = 10;
		[SerializeField] private float _speed = 3;
		private Point _current;
		private Sequence _animation;

		public Point Current => _current;
		public float MaxDistance => _maxDisance;

		public void MoveTo(Point point, Action onCompleteCallback = null) {
			if (point.IsOccupied) {
				throw new System.InvalidOperationException("Target point is occuped.");
			}
			
			_animation?.Kill();
			_current?.OnExit(this);
			_current = point;

			var time = Vector3.Distance(point.transform.position, transform.position) / _speed;
			_animation = DOTween.Sequence()
				.Join(transform.DOMove(point.transform.position, time))
				.Join(transform.DORotate(point.transform.eulerAngles, time))
				.OnComplete(() => {
					OnPointReached();
					onCompleteCallback?.Invoke();
				});
			_animation.Play();

		}
		public void AttachTo(Point point) {
			_current?.OnExit(this);
			_current = point;
			transform.parent = point.transform;
			transform.position = point.transform.position;
			transform.rotation = point.transform.rotation;
			_current?.OnEnter(this);
		}
		public bool CanMoveTo(Point point) {
			return point.IsOccupied == false && Vector3.Distance(point.transform.position, transform.position) <= _maxDisance;
		}

		public void SetMaxDist(float dist) {
			_maxDisance = Mathf.Max(0, dist);
		}

		private void OnPointReached() {
			transform.SetParent(_current.transform);
			_current?.OnEnter(this);
		}
	}
}