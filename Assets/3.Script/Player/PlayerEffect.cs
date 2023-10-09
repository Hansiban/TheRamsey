using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void WalkEffect(float h)
    {
        animator.SetFloat("Horizontal", h);
    }
    public void JumpEffect(bool isJump)
    {
        animator.SetBool("isJump", isJump);
    }
    public void LandEffect(bool isLand)
    {
        animator.SetBool("isLand",isLand);
    }
}
