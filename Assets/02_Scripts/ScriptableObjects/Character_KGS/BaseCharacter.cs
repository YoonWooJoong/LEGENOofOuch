using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    AnimationHandler animHandle;
    [SerializeField] protected Transform target;

    [Header("Stat")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackPower, attackSpeed;

    protected float CurHp { get => CurHp; set => Mathf.Clamp(value, 0, maxHp); }
    protected bool IsMove => moveDir.magnitude > 0;
    protected bool isAttacking;

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animHandle = GetComponent<AnimationHandler>();
    }

    protected virtual void Update()
    {
        SetDir();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        animHandle.Move(moveDir);
        rig.velocity = moveDir * speed;
    }

    protected virtual void SetDir()
    {
        if (target != null && !IsMove)
            lookDir = (target.position - transform.position).normalized;
        else if (IsMove)
            lookDir = moveDir;

        float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        sprite.flipX = Mathf.Abs(rotZ) > 90f;
    }

    protected virtual void Attack()
    {
        if(isAttacking)
        {
            animHandle.Attack();
        }
    }
}
