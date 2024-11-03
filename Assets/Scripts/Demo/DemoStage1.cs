using Game.Characters;
using Game.Characters.Ghost;
using Game.GameCamera;
using Game.GameFlow;
using Game.PointBasedMovement;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Demo {
	public class DemoStage1: DemoGameState {
		[SerializeField] private GhostPool _pool;
		[SerializeField] private Point _ghostPoint;
		[SerializeField] private Character _player;
		[SerializeField] private Point _playerPoint;
		[Space]
		[SerializeField] private TurnMachine _turnMachine;
		[SerializeField] private GameObject _ui;
		[Space]
		[SerializeField] private CameraControl _cameraControl;
		[SerializeField] private Transform _defaultCameraState;
		[SerializeField] private CinemachineOrbitalFollow _camera;
		[Space]
		[SerializeField] private GameObject[] _disableOnEnter;

		private void Next() {
			Game.Change<DemoStage2>();
		}

		public override void OnEnter() {
			_camera.ForceCameraPosition(_defaultCameraState.position, _defaultCameraState.rotation);
			_cameraControl.enabled = false;

			_disableOnEnter.ToList().ForEach((o) => o.SetActive(false));
			_pool.AllDied += Next;
			_pool.SpawnAt(_ghostPoint).Movement.SetMaxDist(0);
			
			_player.Movement.AttachTo(_playerPoint);
			
			_ui.SetActive(true);
			
			_turnMachine.NextTurn();
		}
		public override void OnExit() {
			_pool.AllDied -= Next;
			_ui.SetActive(false);
		}
	}
}