using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaController : MonoBehaviour
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
    {   
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            button[i].onClick.AddListener(() => OnClickButton(index));
        }
        gacha = GachaManager.Instance.gacha;
    }
    private void Start()
    {
        if (GachaManager.Instance == null)
        {
            Debug.LogError("GachaManager.Instance is NULL. Ensure GachaManager exists in the scene.");
            return;
        }
        
    }

    /// <summary>
    /// 외부에서 가챠를 실행하는 함수 
    /// </summary>
    public void StartGacha()
    {
        StartCoroutine(HandleGacha());
    }

    /// <summary>
    /// 가챠 실행을 처리하는 코루틴
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 슬롯에 바운스 효과를 주는 코루틴
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 레어 확률일 때 기둥 색상 변경
    /// 일반 확률일 때 기둥 색상 변경   
    /// </summary>
    /// <param name="color"></param>
    private void ChangePillarColor(Color color)
    {
            pillarMaterial.SetColor("_Color", color);
    }

    /// <summary>
    /// 능력 이름과 설명을 가져오는 함수
    /// </summary>
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

    /// <summary>
    /// 플레이어가 버튼을 누르면 스킬번호를 반환해주는 함수
    /// 실행이후 가챠가 종료된다
    /// </summary>
    /// <param name="bottonSelect"></param>
    public void OnClickButton(int bottonSelect)
    {
        AbilityEnum[] selectedAbility = gacha.GetSelectedAbility();
        gacha.gachaAbilityController.UpgradeAbility(selectedAbility[bottonSelect]);
        GachaManager.Instance.GachaSelect(selectedAbility[bottonSelect]);
        init();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void init()
    {
        Piller.SetActive(true);
        commonBackground.SetActive(false);
        rareBackground.SetActive(false);
        backGround.SetActive(false);
    }
}
