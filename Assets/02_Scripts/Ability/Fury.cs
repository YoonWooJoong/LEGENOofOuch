using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Fury : AbilityBase
{
    private float damageIncreasePerPercent; // 잃은 체력 1%당 공격력 증가량
    private float furyAtkBonus = 0f; // Fury 스킬로 증가하는 추가 공격력
    private Coroutine furyCoroutine; // 코루틴 저장용 변수

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;

        // 기존에 실행 중인 코루틴이 있다면 중지
        if (furyCoroutine != null)
        {
            StopCoroutine(furyCoroutine);
        }
        furyCoroutine = StartCoroutine(UpdateFuryDamage(player));
    }

    protected override void UpdateAbility()
    {
        damageIncreasePerPercent = abilityData.values[isUpgraded ? 1 : 0] * 0.01f;
    }

    private IEnumerator UpdateFuryDamage(PlayerCharacter player)
    {
        while (player != null && player.GetCurHp() > 0)
        {
            // 현재 체력에 따른 추가 공격력 계산
            float lostHpPercent = (1 - (player.GetCurHp() / player.MaxHp)) * 100;
            float newFuryAtkBonus = lostHpPercent * damageIncreasePerPercent;

            // 기존 Fury 보너스를 제거 후 새 값을 추가
            player.AtkBuf -= furyAtkBonus;
            furyAtkBonus = newFuryAtkBonus;
            player.AtkBuf += furyAtkBonus;

            Debug.Log($" 잃은 체력: {lostHpPercent}%, Fury 추가 공격력: {furyAtkBonus * 100}%");

            yield return new WaitForSeconds(0.5f); // 0.5초마다 업데이트
        }
    }
}