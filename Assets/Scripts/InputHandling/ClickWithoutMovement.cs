using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.InputHandling {
	public class ClickWithoutMovement: MonoBehaviour {
		[SerializeField] private float _maxCursorMovementThreshold = 10;
		[SerializeField] private InputAction _clickAction;

		private Vector2 _initialMousePosition;
		private bool _isMouseHeld;

		public event Action Performed;

		private void OnDown(InputAction.CallbackContext context) {
			_initialMousePosition = Mouse.current.position.ReadValue();
			_isMouseHeld = true;
		}
		private void OnUp(InputAction.CallbackContext context) {
			if (_isMouseHeld) {
				Vector2 finalMousePosition = Mouse.current.position.ReadValue();
				if (Vector2.Distance(_initialMousePosition, finalMousePosition) < _maxCursorMovementThreshold) {
					Performed?.Invoke();
				}
				_isMouseHeld = false;
			}
		}

		private void OnEnable() {
			_clickAction.Enable();
			_clickAction.started += OnDown;
			_clickAction.canceled += OnUp;
		}
		private void OnDisable() {
			_clickAction.started -= OnDown;
			_clickAction.canceled -= OnUp;
			_clickAction.Disable();
		}
	}
}
