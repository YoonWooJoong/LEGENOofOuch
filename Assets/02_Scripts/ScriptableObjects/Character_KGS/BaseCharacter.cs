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
    protected bool IsMove => moveDir.magnitude > 0.5f;

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;

    protected bool IsAttacking => !IsMove && target != null;
    float AttackDelay => 1 / attackSpeed;
    float timeSinceLastAttack = float.MaxValue;

    protected float TargetDis => target == null ? float.MaxValue : (target.position - transform.position).magnitude;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animHandle = GetComponent<AnimationHandler>();
    }

    protected virtual void Update()
    {
        HandleAction();
        SetDir();
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 오버라이드시 이동방향을 이곳에서 결정합니다. 이동방향은 반드시 normalize를 마지막에 해 주어야 합니다.
    /// </summary>
    protected virtual void HandleAction()
    { }

    protected virtual void Move()
    {
        animHandle?.Move(moveDir);

        if (rig != null)
            rig.velocity = moveDir * speed;
    }

    /// <summary>
    /// 이동시에는 이동방향을, 그 외에는 목표물을 바라보도록 합니다.
    /// </summary>
    protected virtual void SetDir()
    {
        if (target != null && !IsMove)
            lookDir = (target.position - transform.position).normalized;
        else if (IsMove)
            lookDir = moveDir;

        if (lookDir.x != 0)
            sprite.flipX = lookDir.x < 0;
    }

    /// <summary>
    /// 공격속도를 반영해 다음 공격까지 공격을 대기합니다.
    /// </summary>
    void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= AttackDelay)
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    /// <summary>
    /// 공격시 행동을 정의합니다.
    /// </summary>
    protected virtual void Attack()
    {
        animHandle?.Attack();
    }
}
