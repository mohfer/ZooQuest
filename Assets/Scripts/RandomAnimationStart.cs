using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.shortNameHash, 0, Random.value);
    }
}