using UnityEngine;

namespace Game.Demo {
	public abstract class DemoGameState: MonoBehaviour {
		protected DemoGame Game { get; private set; }

		public void SetGame(DemoGame game) { 
			Game = game; 
		}

		public abstract void OnEnter();
		public abstract void OnExit();
	}
}