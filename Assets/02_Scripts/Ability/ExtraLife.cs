using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ExtraLife : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;
        player.life++;
        Debug.Log("¸ñ¼û Ãß°¡");
    }
}