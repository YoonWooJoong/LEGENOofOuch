using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] float attackRange;

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
        lookDir = (target.transform.position - transform.position).normalized;
        if (TargetDis > attackRange)
            moveDir = lookDir;
        else
            moveDir = Vector2.zero;
    }
}
