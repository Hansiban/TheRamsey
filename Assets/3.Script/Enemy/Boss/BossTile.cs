using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossTile : MonoBehaviour
{
    [Header("Damage")]
    private GameObject boss_;
    private Boss boss;

    [Header("ETC")]
    public bool canDamage;
    private Animator animator;
    private PlayerControll player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boss_ = GameObject.FindGameObjectWithTag("Enemy");
        boss = boss_.GetComponent<Boss>();
    }

    private void Damage()
    {
        animator.SetBool("destroy", true);
        boss.Damage();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!canDamage)
            {
                animator.SetBool("destroy", true);
            }
        }

        if (col.gameObject.tag == "Enemy")
        {
            if (canDamage)
            {
                Damage();
            }
        }

        if (col.gameObject.tag == "Player" && !canDamage)
        {
            player.Damage();
        }
    }
}
