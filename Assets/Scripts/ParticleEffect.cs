using System.Collections;
using UnityEngine;

namespace Game {
	public class ParticleEffect: MonoBehaviour {
		[SerializeField] private ParticleSystem _particleSystem;

		public void PlayEffect() {
			transform.SetParent(null);
			_particleSystem.Play();
			StartCoroutine(WaitForParticlesToStop());
		}

		private IEnumerator WaitForParticlesToStop() {
			while (!_particleSystem.isStopped) {
				yield return null;
			}
			Destroy(gameObject);
		}
	}
}
