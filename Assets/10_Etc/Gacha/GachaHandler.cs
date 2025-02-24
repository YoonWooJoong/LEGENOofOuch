using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GachaHandler : MonoBehaviour
{
    public Gacha gacha;
    public GachaAnimation gachaAnimation;
    public RectTransform[] skillSlots;
    public Image backgroundEffect;
    public Material pillarMaterial; // 기둥 머티리얼
    public float bounceScale = 1.2f;
    public float bounceDuration = 0.2f;

    // 색상 설정
    private Color normalColor = Color.green;  // 기본 초록색
    private Color rareColor = Color.yellow;   // 레어 확률일 때 노란색

    void Start()
    {
        gacha.SelectRandomIcons();
        StartCoroutine(HandleGacha());
    }

    private IEnumerator HandleGacha()
    {
        yield return StartCoroutine(gachaAnimation.AnimatePachinko());

        bool isRare = false;
        Sprite[] selectedIcons = gacha.GetSelectedIcons();
        foreach (var icon in selectedIcons)
        {
            if (gacha.IsRare(icon))
            {
                isRare = true;
                break;
            }
        }

        if (isRare)
        {
            yield return StartCoroutine(PlayRareEffect());
        }

        // 2초 후 색상 변경
        ChangePillarColor(isRare);

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            skillSlots[i].GetComponent<Image>().sprite = selectedIcons[i];
            skillSlots[i].GetComponent<Image>().color = isRare ? rareColor : normalColor;
            StartCoroutine(PlayBounceEffect(skillSlots[i]));
        }
    }

    private IEnumerator PlayRareEffect()
    {
        backgroundEffect.color = new Color(1f, 1f, 0.5f, 0.8f);
        backgroundEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        backgroundEffect.gameObject.SetActive(false);
    }

    private IEnumerator PlayBounceEffect(RectTransform slot)
    {
        Vector3 originalScale = slot.localScale;
        Vector3 targetScale = originalScale * bounceScale;
        float elapsedTime = 0f;

        while (elapsedTime < bounceDuration)
        {
            elapsedTime += Time.deltaTime;
            slot.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / bounceDuration);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < bounceDuration)
        {
            elapsedTime += Time.deltaTime;
            slot.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / bounceDuration);
            yield return null;
        }
    }

    private void ChangePillarColor(bool isRare)
    {
        if (pillarMaterial != null)
        {
            Color targetColor = isRare ? rareColor : normalColor;
            pillarMaterial.SetColor("_GradientColor", targetColor);
        }
    }
}
