using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SpeedBoost : AbilityBase
{
    private float cooldownTime;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;
        StartCoroutine(BoostAttackSpeed(player));
    }

    protected override void UpdateAbility()
    {
        cooldownTime = isUpgraded ? abilityData.values[1] : abilityData.values[0];
    }


    private IEnumerator BoostAttackSpeed(PlayerCharacter player)
    {
        while (true)
        {
            player.AsBuf += 0.625f;

            yield return new WaitForSeconds(2);

            player.AsBuf -= 0.625f;

            yield return new WaitForSeconds(cooldownTime); // {n}√  ¥Î±‚
        }
    }
}