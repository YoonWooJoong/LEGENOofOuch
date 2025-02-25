using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Invincibility : AbilityBase
{
    private float cooldownTime;
    private Coroutine invincibilityCoroutine; // 코루틴 저장 변수

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;
        // 기존 코루틴이 있다면 중지
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(ActivateInvincibility(player));
    }

    protected override void UpdateAbility()
    {
        cooldownTime = abilityData.values[isUpgraded ? 1 : 0];
    }

    /// <summary>
    /// 2초 동안 무적 상태 적용 후, n초 후 다시 실행
    /// </summary>
    private IEnumerator ActivateInvincibility(PlayerCharacter player)
    {
        while (player != null && player.GetCurHp() > 0)
        {
            player.GodMod = true; // 무적 상태 활성화
            Debug.Log("무적 시작");

            yield return new WaitForSeconds(2); // 2초 무적 유지

            player.GodMod = false; // 무적 상태 해제
            Debug.Log("무적 종료");

            yield return new WaitForSeconds(cooldownTime); // {n}초 대기
        }
    }
}