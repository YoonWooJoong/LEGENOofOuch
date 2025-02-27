using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange = 3;

    [Header("")]
    [SerializeField] MonsterEnum mEnum;
    [SerializeField] GameObject potionPrefeb;
    [SerializeField][Range(0, 100)] float potionDrop;

    protected override bool IsAttacking => base.IsAttacking && TargetDis <= attackRange;

    NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        agent.speed = Speed;
    }

    void Start()
    {
        target = FindAnyObjectByType(typeof(PlayerCharacter)).GameObject().transform;
    }

    /// <summary>
    /// 공격사거리보다 플레이어가 멀리 있으면 플레이어 쪽으로 이동합니다. 아니면 그자리에서 공격하도록 합니다.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();
        if (target == null)
            return;

        Vector2 deltaP = agent.nextPosition - transform.position;
        if (TargetDis > attackRange && deltaP.magnitude < attackRange*2)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
        moveDir = (agent.nextPosition - transform.position).normalized;
    }

    /// <summary>
    /// 적은 사망시 확률적으로 포션을 하나 드랍하고, 남은 적 리스트에서 제외됩니다.
    /// </summary>
    protected override void Death()
    {
        if (Random.Range(0, 100) < potionDrop)
            Instantiate(potionPrefeb, transform.position, Quaternion.identity);

        GameManager.Instance.KillMonster(this);
        base.Death();
    }

    protected override void Attack()
    {
        base.Attack();
        GameManager.Instance.ProjectileManager.ShootEnemyProjectile(transform.position, LookDir, AttackPower);
    }
}
