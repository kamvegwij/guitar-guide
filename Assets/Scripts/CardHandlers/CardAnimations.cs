using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardAnimations : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AnimateCard()
    {
        animator.Play("CardFlipAnim", 0, 0f);
    }
    public void PlayShakeAnimation()
    {
        animator.Play("ShakeAnim", 0, 0f);
    }
    public void PlayMatchedAnimation()
    {
        animator.Play("MatchedAnim", 0, 0f);
        //animator.StopPlayback();
    }
}
