using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameFlow {
	public class TurnMachine: MonoBehaviour {
		private Queue<ITurn> _turns = new Queue<ITurn>();
		private ITurn _current;

		public void NextTurn() {
			if (_current != null) {
				_current.OnExit();
				_turns.Enqueue(_current);
				_current = null;
			}
			_current = _turns.Dequeue();
			_current.OnEnter();
		}

		public void Add(ITurn turn) {
			_turns.Enqueue(turn);
		}
	}
}