using System.Collections;
using UnityEngine;

public class DetectorController : ObstacleController {

	public float speed;
	
	private Animator animator;
	private static readonly int IsShrinkAnimator = Animator.StringToHash("IsShrink");
	private static readonly int PlayAnimator = Animator.StringToHash("Play");

	private void Start ()
	{
		animator = GetComponent<Animator>();
		if (speed > 0)
		{
			animator.SetBool(IsShrinkAnimator, true);
			PlayAnimation();
		}
	}

	public IEnumerator OnEndShrinkAnimation()
	{
		animator.speed = 1f;
		animator.SetBool(IsShrinkAnimator, false);
		yield return Wait();
		PlayAnimation();
	}

	public IEnumerator OnEndExpandAnimation()
	{
		animator.speed = 1f;
		animator.SetBool(IsShrinkAnimator, true);
		yield return Wait();
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		animator.speed = speed;
		animator.SetTrigger(PlayAnimator);
	}
}
