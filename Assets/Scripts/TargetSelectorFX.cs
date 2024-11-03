using UnityEngine;

namespace Game {
	public class TargetSelectorFX: MonoBehaviour {
		[SerializeField] private AudioClip _selected;
		[SerializeField] private AudioClip _rejected;
		[SerializeField] private AudioSource _audioSource;

		public void PlaySelected() {
			_audioSource.PlayOneShot(_selected);
		}
		public void PlayRejected() {
			_audioSource.PlayOneShot(_rejected);
		}
	}
}