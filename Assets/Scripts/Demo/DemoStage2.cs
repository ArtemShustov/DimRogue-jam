using Game.Characters.Ghost;
using Game.GameCamera;
using Game.PointBasedMovement;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Demo {
	public class DemoStage2: DemoGameState {
		[SerializeField] private GhostPool _pool;
		[SerializeField] private Point[] _spawnPoints;
		[SerializeField] private CameraControl _cameraControl;
		[SerializeField] private GameObject[] _enableOnEnter;
		[Space]
		[SerializeField] private GameObject _ui;
		[SerializeField] private GameObject _cameraHint;
		[SerializeField] private InputAction _click;

		private void Next() {
			Game.Change<DemoStage3>();
		}

		private void OnClick(InputAction.CallbackContext context) {
			_cameraHint.SetActive(false);
		}

		public override void OnEnter() {
			_cameraHint.SetActive(true);
			_ui.SetActive(true);
			_click.performed += OnClick;

			_pool.AllDied += Next;
			_enableOnEnter.ToList().ForEach((o) => o.SetActive(true));
			_spawnPoints.ToList().ForEach((point) => _pool.SpawnAt(point));
			_cameraControl.enabled = true;
		}
		public override void OnExit() {
			_ui.SetActive(false);
			_pool.AllDied -= Next;
			_click.performed -= OnClick;
		}
	}
}