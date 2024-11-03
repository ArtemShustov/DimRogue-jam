using UnityEngine;

namespace Game.GameCamera {
	public class CameraTarget: MonoBehaviour {
		[field: SerializeField] public float Radius { get; private set; } = 5;
		[field: SerializeField] public Transform Root { get; private set; }
	}
}