using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameCamera {
	public class CameraControl: MonoBehaviour {
		[SerializeField] private InputAction _input;
		[SerializeField] private CinemachineInputAxisController _camera;

		private void Start() {
			_camera.enabled = false;
		}

		private void Enable(InputAction.CallbackContext obj) {
			_camera.enabled = true;
		}
		private void Disable(InputAction.CallbackContext obj) {
			_camera.enabled = false;
		}

		private void OnEnable() {
			_input.performed += Enable;
			_input.canceled += Disable;
			_input.Enable();
		}
		private void OnDisable() {
			_input.performed -= Enable;
			_input.canceled -= Disable;
			_input.Disable();
		}
	}
}