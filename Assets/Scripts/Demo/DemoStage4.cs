using UnityEngine;

namespace Game.Demo {
	public class DemoStage4: DemoGameState {
		[SerializeField] private GameObject _ui;

		public override void OnEnter() {
			_ui.SetActive(true);
			Debug.Log("Thanks for playing!");
		}
		public override void OnExit() { }
	}
}