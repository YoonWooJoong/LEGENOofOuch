using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class FireOrb : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        GameManager.Instance.ProjectileManager.CreateFireOrb(GameManager.Instance.player.transform.position);
    }
}