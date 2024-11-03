using Game.Characters;
using Game.Characters.Ghost;
using Game.GameFlow;
using Game.PointBasedMovement;
using UnityEngine;

namespace Game.Demo {
	public class TestDemoState: DemoGameState {
		[SerializeField] private Character _player;
		[SerializeField] private Point _playerRoot;
		[Space]
		[SerializeField] private Point _ghostRoot;
		[SerializeField] private GhostPool _pool;
		[Space]
		[SerializeField] private TurnMachine _turnMachine;

		private void Next() {
			Game.Change<TestDemoState>();
		}

		public override void OnEnter() {
			_pool.AllDied += Next;
			_pool.SpawnAt(_ghostRoot).Movement.SetMaxDist(0);
			_player.Movement.AttachTo(_playerRoot);
			_turnMachine.NextTurn();
			Debug.Log("TestDemoState.OnEnter()");
		}

		public override void OnExit() {
			Debug.Log("TestDemoState.OnExit()");
		}
	}
}