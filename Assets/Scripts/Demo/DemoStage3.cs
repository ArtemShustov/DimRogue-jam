using System.Linq;
using UnityEngine;

namespace Game.Demo {
	public class DemoStage3: DemoGameState {
		[SerializeField] private GameObject _ui;
		[SerializeField] private GameObject[] _enableOnEnter;
		[SerializeField] private FinalPoint _final;

		private void Next() {
			Game.Change<DemoStage4>();
		}

		public override void OnEnter() {
			_ui.SetActive(true);
			_enableOnEnter.ToList().ForEach(o => o.SetActive(true));
			_final.Reached += Next;
		}
		public override void OnExit() {
			_ui.SetActive(false);
			_final.Reached -= Next;
		}
	}
}