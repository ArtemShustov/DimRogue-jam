using System;
using System.Collections;
using UnityEngine;

namespace Game.Characters {
	public class Attacker: MonoBehaviour {
		[SerializeField] private int _damage = 1;
		[SerializeField] private float _maxDist = 3;
		[SerializeField] private Animator _animator;
		[SerializeField] private string _attackAnimTrigger = "Attack";

		public void Attack(IAttackable attackable, Action onCompleteCallback = null) {
			if (!CanAttack(attackable)) {
				throw new System.InvalidOperationException("Can't attack target");
			}
			attackable.TakeDamage(_damage);
			_animator.SetTrigger(_attackAnimTrigger);
			StartCoroutine(InvokeNextFrame(onCompleteCallback));
		}
		public bool CanAttack(IAttackable attackable) {
			return Vector3.Distance(transform.position, attackable.gameObject.transform.position) <= _maxDist;
		}

		private IEnumerator InvokeNextFrame(Action action) {
			yield return new WaitForEndOfFrame();
			action?.Invoke();
		}
	}
	public interface IAttackable {
		public GameObject gameObject { get; }
		public void TakeDamage(int points);
	}
}