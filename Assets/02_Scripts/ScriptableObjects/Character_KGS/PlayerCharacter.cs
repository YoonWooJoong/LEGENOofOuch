using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage, criticalChance;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    protected override void Move()
    {

    }
}
