
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GachaAnimation : MonoBehaviour
{
    public Image slotImage;
    public Sprite[] skillIcons;
    public float spinDuration = 2f;
    public float commonSpinDuration = 2f;
    public float rareSpinDuration = 3f;
    public float initialDelay = 0.05f;
    public float finalDelay = 0.3f;

    // Rare로 지정할 인덱스 집합
    AbilityEnum[] rareIndices = new AbilityEnum[] {
        AbilityEnum.BloodThirst, AbilityEnum.Invincibility, AbilityEnum.Blaze,
        AbilityEnum.Spirit, AbilityEnum.Archer, AbilityEnum.Mage, AbilityEnum.Warrior
    };
    public Color commonColor = Color.green;
    public Color rareColor = Color.yellow;

    void Awake()
    {
        GachaController handler = GetComponent<GachaController>();

    }
    /// <summary>
    /// 외부에서 애니메이션을 시작할 때 호출.
    /// selectedAbility: 최종적으로 고정할 능력 인덱스.
    /// isRare: 레어이면 true, 아니면 false.
    /// </summary>
    public void StartSpin(AbilityEnum selectedAbility, bool isRare)
    {
        StartCoroutine(AnimateSlot(selectedAbility, isRare));
    }

    /// <summary>
    /// 슬롯이미지를 일정 시간 간격으로 교체하며 스핀 애니메이션을 재생.
    /// </summary>
    /// <param name="selectedAbility">현재 슬롯에 들어가는 능력</param>
    /// <param name="isRare">레어인지 아닌지 판별</param>
    /// <returns></returns>
    private IEnumerator AnimateSlot(AbilityEnum selectedAbility, bool isRare)
    {
        // 만약 레어라면, 두 단계(일반 → 레어)로 진행
        if (isRare)
        {
            List<Sprite> nonRareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, (AbilityEnum)i) < 0)
                    nonRareSprites.Add(skillIcons[i]);
            }
            Sprite[] commonSprites = nonRareSprites.ToArray();
            if (commonSprites.Length == 0)
                yield break;
            
            int currentIndex = Random.Range(0, commonSprites.Length);
            float elapsed = 0f;
            while (elapsed < commonSpinDuration)
            {
                slotImage.sprite = commonSprites[currentIndex];
                currentIndex = (currentIndex + 1) % commonSprites.Length;
                float t = elapsed / commonSpinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, initialDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }

            List<Sprite> rareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, (AbilityEnum)i) >= 0)
                    rareSprites.Add(skillIcons[i]);
            }
            Sprite[] rareSpriteArray = rareSprites.ToArray();
            if (rareSpriteArray.Length == 0)
                yield break;

            currentIndex = Random.Range(0, rareSpriteArray.Length);
            elapsed = 0f;
            while (elapsed < rareSpinDuration)
            {
                slotImage.sprite = rareSpriteArray[currentIndex];
                currentIndex = (currentIndex + 1) % rareSpriteArray.Length;
                float t = elapsed / rareSpinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, finalDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }
        }
        else
        {
            List<Sprite> nonRareSprites = new List<Sprite>();
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (System.Array.IndexOf(rareIndices, (AbilityEnum)i) < 0)
                    nonRareSprites.Add(skillIcons[i]);
            }
            Sprite[] commonSprites = nonRareSprites.ToArray();
            if (commonSprites.Length == 0)
                yield break;

            int currentIndex = Random.Range(0, commonSprites.Length);
            float elapsed = 0f;
            while (elapsed < spinDuration)
            {
                slotImage.sprite = commonSprites[currentIndex];
                currentIndex = (currentIndex + 1) % commonSprites.Length;
                float t = elapsed / spinDuration;
                float currentDelay = Mathf.Lerp(initialDelay, finalDelay, t);
                yield return new WaitForSeconds(currentDelay);
                elapsed += currentDelay;
            }
        }

        // 최종 결과: selectedAbility 값에 해당하는 스프라이트로 고정
        // (이때 selectedAbility는 전체 배열(AbilityIcons)에서 유효한 인덱스여야 함)
        if ((int)selectedAbility < 0 || (int)selectedAbility >= skillIcons.Length)
        {
            Debug.LogWarning("selectedAbility out of range. Using index 0.");
            selectedAbility = 0;
        }
        slotImage.sprite = skillIcons[(int)selectedAbility];
    }
}

