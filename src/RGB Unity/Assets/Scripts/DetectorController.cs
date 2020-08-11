using System.Collections;
using UnityEngine;

public class DetectorController : ObstacleController {

	public float speed;
	
	private Animator _animator;
	private static readonly int IsShrinkAnimator = Animator.StringToHash("IsShrink");
	private static readonly int PlayAnimator = Animator.StringToHash("Play");

	private void Start ()
	{
		_animator = GetComponent<Animator>();
		if (speed > 0)
		{
			_animator.SetBool(IsShrinkAnimator, true);
			PlayAnimation();
		}
	}

	public IEnumerator OnEndShrinkAnimation()
	{
		_animator.speed = 1f;
		_animator.SetBool(IsShrinkAnimator, false);
		yield return Wait();
		PlayAnimation();
	}

	public IEnumerator OnEndExpandAnimation()
	{
		_animator.speed = 1f;
		_animator.SetBool(IsShrinkAnimator, true);
		yield return Wait();
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		_animator.speed = speed;
		_animator.SetTrigger(PlayAnimator);
	}
}
