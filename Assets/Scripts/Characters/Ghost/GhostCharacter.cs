using Game.PointBasedMovement;
using System;
using UnityEngine;

namespace Game.Characters.Ghost {
	public class GhostCharacter: Character {
		[SerializeField] private GhostAttackable _ghostAttackable;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private MeshRenderer _meshRenderer;
		private GhostPool _pool;

#if UNITY_EDITOR
		private void Update() {
			if (Input.GetKeyDown(KeyCode.K)) {
				OnDie();
				Destroy(gameObject);
			}
		}
#endif

		public override bool TryAct(Point point, Action onCompleteCallback = null) {
			_audioSource.PlayOneShot(_audioSource.clip);
			return base.TryAct(point, onCompleteCallback);
		}
		public void SetState(State state) {
			var color = state switch {
				State.Agressive => Color.red,
				State.Protected => Color.blue,
				_ => Color.white,
			};
			_meshRenderer.material.color = color;
			_meshRenderer.material.SetColor("_EmissionColor", color * 3);
			_ghostAttackable.SetState(state);
		}
		public void SetRandomState() {
			var state = (State)UnityEngine.Random.Range(0, 3);
			SetState(state);
		}
		public void SpawnClone() {
			_pool.SpawnNear(this.transform);
		}
		public void SetPool(GhostPool pool) {
			_pool = pool;
		}

		private void OnDie() {
			_pool.OnDie(this);
		}
		private void OnEnable() {
			_ghostAttackable.Died += OnDie;
		}
		private void OnDisable() {
			_ghostAttackable.Died -= OnDie;
		}

		public enum State {
			Neutral, Agressive, Protected
		}
	}
}