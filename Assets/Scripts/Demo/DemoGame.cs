using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Demo {
	public class DemoGame: MonoBehaviour {
		[SerializeField] private DemoGameState _initState;

		private Dictionary<Type, DemoGameState> _states = new Dictionary<Type, DemoGameState>();
		private DemoGameState _current;

		private void Awake() {
			foreach (var state in GetComponents<DemoGameState>()) {
				_states.Add(state.GetType(), state);
				state.SetGame(this);
			}
		}
		private void Start() {
			SetState(_initState);
		}

		public void SetState(DemoGameState state) {
			_current?.OnExit();
			_current = state;
			_current?.OnEnter();
		}
		public void Change<T>() where T: DemoGameState {
			var state = _states[typeof(T)];
			SetState(state);
		}
	}
}