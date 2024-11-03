using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameCamera {
	public class CameraTargetSelector: MonoBehaviour {
		[SerializeField] private LayerMask _mask;
		[SerializeField] private InputAction _input;
		[SerializeField] private Camera _camera;
		[SerializeField] private CinemachineCamera _cinemachineCamera;
		[SerializeField] private CinemachineOrbitalFollow _follow;

		private void Start() {
			_input.Enable();
		}

		public void SetTarget(CameraTarget target) {
			_cinemachineCamera.Follow = target.Root;
			_follow.Radius = target.Radius;
		}

		private void OnClick(InputAction.CallbackContext obj) {
			var ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, maxDistance: 50, layerMask: _mask)) { 
				if (hit.collider.TryGetComponent<CameraTarget>(out var target)) {
					SetTarget(target);
				}
			}
		}

		private void OnEnable() {
			_input.performed += OnClick;
		}
		private void OnDisable() {
			_input.performed -= OnClick;
		}
	}
}