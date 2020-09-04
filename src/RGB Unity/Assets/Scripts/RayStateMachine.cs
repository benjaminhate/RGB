using UnityEngine;

public class RayStateMachine : StateMachineBehaviour
{
    private static readonly int RandomHaloAnimator = Animator.StringToHash("RandomHalo");

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(RandomHaloAnimator, Random.Range(0,3));
    }
}
