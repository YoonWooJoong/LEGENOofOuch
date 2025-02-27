using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Invincibility : AbilityBase
{
    [SerializeField] private Material invincibleMat;
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
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
        spriteRenderer = player.GetPlayerSpriteRenderer();
        originalMat = player.GetPlayerSpriteRenderer().material;
        invincibilityCoroutine = StartCoroutine(ActivateInvincibility(player));
    }

    protected override void UpdateAbility()
    {
        cooldownTime = isUpgraded ? abilityData.values[1] : abilityData.values[0];
    }

    /// <summary>
    /// 2초 동안 무적 상태 적용 후, n초 후 다시 실행
    /// </summary>
    private IEnumerator ActivateInvincibility(PlayerCharacter player)
    {
        while (player != null && player.CurHp > 0)
        {
            player.GodMod = true; // 무적 상태 활성화

            StartCoroutine(BlinkEffect(player, invincibleMat, originalMat)); // 깜빡이는 효과

            yield return new WaitForSeconds(2); // 2초 무적 유지

            player.GodMod = false; // 무적 상태 해제
            spriteRenderer.material = originalMat; // 원래 머티리얼 복원

            yield return new WaitForSeconds(cooldownTime); // {n}초 대기
        }
    }

    private IEnumerator BlinkEffect(PlayerCharacter player, Material blinkMat, Material originalMat)
    {
        for (int i = 0; i < 6; i++) // 6번 깜빡이게 설정
        {
            spriteRenderer.material = (i % 2 == 0) ? blinkMat : originalMat;
            yield return new WaitForSeconds(0.15f); // 0.15초마다 깜빡이기
        }

        spriteRenderer.material = originalMat; // 원래 머티리얼로 복구
    }
}