using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage, criticalChance;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    protected override void HandleAction()
    {
        base.HandleAction();
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        SearchTarget();
    }

    void SearchTarget()
    {
        var enemys = FindObjectsOfType(typeof(EnemyCharacter));
        foreach (var enemy in enemys)
        {
            float distance = (enemy.GameObject().transform.position - transform.position).magnitude;

            if (distance < TargetDis)
                target = enemy.GameObject().transform;
        }
    }
}
