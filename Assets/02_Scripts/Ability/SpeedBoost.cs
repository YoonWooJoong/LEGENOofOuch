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
        cooldownTime = abilityData.values[isUpgraded ? 1 : 0];
    }


    private IEnumerator BoostAttackSpeed(PlayerCharacter player)
    {
        while (true)
        {
            player.AsBuf += 0.625f;
            Debug.Log("공격 속도 증가 시작");

            yield return new WaitForSeconds(2);

            player.AsBuf -= 0.625f;
            Debug.Log("공격 속도 증가 종료");

            yield return new WaitForSeconds(cooldownTime); // {n}초 대기
        }
    }
}