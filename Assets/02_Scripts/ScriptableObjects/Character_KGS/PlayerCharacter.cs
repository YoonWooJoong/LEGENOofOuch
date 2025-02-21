using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage, criticalChance;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;

    protected override void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        base.Update();
    }
}
