using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    AnimationHandler animHandle;
    [SerializeField] public Transform target;
    [SerializeField] Slider HpBar;

    public SpriteRenderer Sprite => sprite;

    [Header("BaseStat")]
    [SerializeField] float maxHp = 100;
    [SerializeField] float speed = 2;
    [SerializeField] float attackPower = 1, attackSpeed = 1;

    public virtual float MaxHp => maxHp;
    public virtual float Speed => speed;
    public virtual float AttackPower => attackPower;
    public virtual float AttackSpeed => attackSpeed;

    float curHp;
    public float CurHp { get => curHp; protected set => curHp = Mathf.Clamp(value, 0, MaxHp); }

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;
    public Vector2 LookDir => lookDir;
    public Vector2 MoveDir => moveDir;
    protected bool IsMove => MoveDir.magnitude > 0.5f;
    protected float TargetDis => target == null ? float.MaxValue : (target.position - transform.position).magnitude;

    protected virtual bool IsAttacking => !IsMove && target != null;
    float AttackDelay => 1 / AttackSpeed;
    float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animHandle = GetComponent<AnimationHandler>();
        curHp = MaxHp;
    }

    protected virtual void Update()
    {
        HandleAction();
        SetDir();
        HandleAttackDelay();
        Debug.DrawRay(transform.position, lookDir, Color.red);
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 입력/조건에 따른 다음 행동을 결정합니다. 주로 이동방향의 결정 등이 있습니다.
    /// </summary>
    protected virtual void HandleAction()
    { }

    protected virtual void Move()
    {
        animHandle?.Move(moveDir);

        if (rig != null)
            rig.velocity = moveDir * Speed;
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
        animHandle?.Attack(AttackSpeed);
        //투사체 발사시 나오는 효과음
        SoundManager.instance.PlaySFX("");
        //gamemaneger.~~~
    }

    /// <summary>
    /// 캐릭터의 현제 체력을 변경합니다.
    /// </summary>
    /// <param name="change">변경할 수치입니다. 데미지면 음수, 회복이면 양수값을 입력합니다.</param>
    public virtual void ChangeHealth(float change)
    {
        //피격/회복시 나오는 효과음
        string changeSfx = change >= 0 ? "" :"";
        SoundManager.instance.PlaySFX(changeSfx);

        CurHp += change;
        HpBar.value = CurHp / MaxHp;
        if (CurHp == 0f)
            Death();
    }

    /// <summary>
    /// 캐릭터가 사망하면 그자리에 정지하고 잠시 후 사라집니다.
    /// </summary>
    protected virtual void Death()
    {
        //사망시 나오는 효과음
        SoundManager.instance.PlaySFX("");

        rig.velocity = Vector2.zero;

        foreach (var compo in transform.GetComponentsInChildren<Behaviour>())
            compo.enabled = false;

        Destroy(gameObject);
    }

    /// <summary>
    /// 현재 HP 반환
    /// </summary>
    /// <returns></returns>
    public float GetCurHp()
    {
        return CurHp;
    }

    /// <summary>
    /// 현재 바라보는 방향 vector
    /// </summary>
    /// <returns></returns>
    public Vector3 GetlookDir()
    {
        return lookDir;
    }

    public SpriteRenderer GetPlayerSpriteRenderer()
    {
        return sprite;
    }
}
