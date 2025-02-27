using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    static readonly int IsMoving = Animator.StringToHash("isMove");
    static readonly int IsAttack = Animator.StringToHash("attack");
    static readonly int AttackSpeed = Animator.StringToHash("attackSpeed");

    protected Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        anim.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Attack(float attackSpeed)
    {
        anim.SetFloat(AttackSpeed, attackSpeed);
        anim.SetTrigger(IsAttack);
    }
}
