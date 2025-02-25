using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spirit : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        GameManager.Instance.ProjectileManager.CreateFairy(GameManager.Instance.player.transform.position);
    }
}