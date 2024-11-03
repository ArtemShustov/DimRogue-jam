using UnityEngine;

namespace Game.GameFlow {
	public class InitTurnMachine: MonoBehaviour {
		[SerializeField] private PlayerTurn _player;
		[SerializeField] private AITurn _ai;
		[SerializeField] private TurnMachine _machine;

		private void Awake() {
			_machine.Add(_player);
			_machine.Add(_ai);
		}
	}
}