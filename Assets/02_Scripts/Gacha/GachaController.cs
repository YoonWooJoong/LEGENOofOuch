using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaHandler : MonoBehaviour
{
    public Gacha gacha;
    public GachaAnimation[] gachaAnimation;
    public RectTransform[] abilitySlots;
    public Material pillarMaterial; // 기둥 머티리얼
    public TextMeshProUGUI[] abilityName;
    public TextMeshProUGUI[] abilityDescription;
    public GameObject Piller;
    public GameObject commonBackground;
    public GameObject rareBackground;
    public GameObject backGround;
    public Button[] button;
    public float bounceScale = 1.2f;
    public float bounceDuration = 0.2f;

    // 색상 설정
    private Color commonColor = Color.green;  // 기본 초록색
    private Color rareColor = Color.yellow;   // 레어 확률일 때 노란색
    private void Awake() 
    {   for (int i = 0; i < 3; i++)
        {
            int index = i;
            button[i].onClick.AddListener(() => OnClickButton(index));
        }
    }
    private void Start()
    {
        if (GachaManager.Instance == null)
        {
            Debug.LogError("GachaManager.Instance is NULL. Ensure GachaManager exists in the scene.");
            return;
        }
        gacha = GachaManager.Instance.gacha;
    }
    public void StartGacha()
    {
        StartCoroutine(HandleGacha());
    }
    private IEnumerator HandleGacha()
    {
        //yield return StartCoroutine(gachaAnimation.AnimateSlot());

        Piller.SetActive(true);
        ChangePillarColor(commonColor);
        AbilityEnum[] selectedAbility = gacha.GetSelectedAbility();

        bool isRare = gacha.GetIsRare();
        for (int i = 0; i < selectedAbility.Length; i++)
        {
            gachaAnimation[i].StartSpin(selectedAbility[i], isRare);
            //yield return new WaitForSeconds(0.5f);
        }
        // 2초 후 색상 변경
        

        yield return new WaitForSeconds(2f);
        if (!isRare)
        {
            for (int i = 0; i < abilitySlots.Length; i++)
            {
                StartCoroutine(PlayBounceEffect(abilitySlots[i]));
                yield return new WaitForSeconds(0.5f);
            }
            Piller.SetActive(false);
            backGround.SetActive(true);
            GetText();
            commonBackground.SetActive(true);
        }
        else
        {
            ChangePillarColor(rareColor);
            for (int i = 0; i < abilitySlots.Length; i++)
            {
                Debug.Log("i");Debug.Log(abilitySlots[i]);
                StartCoroutine(PlayBounceEffect(abilitySlots[i]));
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < abilitySlots.Length; i++)
            {
                StartCoroutine(PlayBounceEffect(abilitySlots[i]));
                yield return new WaitForSeconds(0.5f);
            }
            Piller.SetActive(false);
            backGround.SetActive(true);
            GetText();
            rareBackground.SetActive(true);
        }
        

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

    private void ChangePillarColor(Color color)
    {
            pillarMaterial.SetColor("_Color", color);
    }
    private void GetText()
    {
        for (int i = 0; i < abilityName.Length; i++)
        {
            abilityName[i].text = GachaManager.Instance.AbilityName[i];
        }
        for (int i = 0; i < abilityDescription.Length; i++)
        {
            abilityDescription[i].text = GachaManager.Instance.Abilitydescription[i];
        }
    }
    public void OnClickButton(int bottonSelect)
    {
        AbilityEnum[] selectedAbility = gacha.GetSelectedAbility();
        GachaManager.Instance.GachaSelect(selectedAbility[bottonSelect]);
        init();
    }
    public void init()
    {
        Piller.SetActive(true);
        commonBackground.SetActive(false);
        rareBackground.SetActive(false);
        backGround.SetActive(false);
    }
}
