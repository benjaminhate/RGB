using UnityEngine;

public class ColorerStateMachine : StateMachineBehaviour
{
    private static readonly int RandomDripAnimator = Animator.StringToHash("RandomDrip");

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(RandomDripAnimator, Random.Range(0,3));
    }
}
