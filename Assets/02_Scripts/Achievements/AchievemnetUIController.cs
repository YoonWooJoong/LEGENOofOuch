using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIController : MonoBehaviour
{
    // UI 요소들
    [SerializeField] private RectTransform achievementUI;
    [SerializeField] private TextMeshProUGUI achievementNameText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;
    [SerializeField] private Image achievementImage;
    [SerializeField] private Sprite[] iconlist;
    //[SerializeField] private TextMeshProUGUI[] achievementPanelName;
    [SerializeField] private TextMeshProUGUI[] achievementPanelDiscription;
    [SerializeField] private Image[] achievementPanelImage;
    public class AchievementData
    {
        public string name;
        public string description;
        public Sprite image;
    }

    // 도전과제 데이터 매핑
    private Dictionary<string, AchievementData> achievements = new Dictionary<string, AchievementData>();

    private void Start()
    {
        // 도전과제 데이터 등록
        achievements.Add("LevelUp", new AchievementData { name = "첫 레벨업!", description = "레벨을 처음으로 올렸습니다.", image = iconlist[0] });
        achievements.Add("Death", new AchievementData { name = "첫 사망!", description = "처음으로 사망하였습니다.", image = iconlist[1] });
        achievements.Add("Ability", new AchievementData { name = "첫 스킬 획득!", description = "새로운 능력을 얻었습니다.", image = iconlist[2] });
        achievements.Add("Trade", new AchievementData { name = "첫 거래!", description = "악마와 처음으로 거래했습니다.", image = iconlist[3] });
        achievements.Add("CastleClear", new AchievementData { name = "첫 성 클리어!", description = "처음으로 성을 클리어했습니다.", image = iconlist[4] });
        achievements.Add("SwampClear", new AchievementData { name = "첫 늪 클리어!", description = "처음으로 늪을 클리어했습니다.", image = iconlist[5] });
        achievements.Add("VolcanoClear", new AchievementData { name = "첫 화산 클리어!", description = "처음으로 화산을 클리어했습니다.", image = iconlist[6] });
    }

    private void OnEnable()
    {
        // 이벤트 등록
        Achievements.OnFirstLevelUp += () => ShowAchievement("LevelUp");
        Achievements.OnFirstDeath += () => ShowAchievement("Death");
        Achievements.OnFirstAbility += () => ShowAchievement("Ability");
        Achievements.OnFirstTradlear += () => ShowAchievement("Trade");
        Achievements.OnFirstCastleClear += () => ShowAchievement("CastleClear");
        Achievements.OnFirstSwampClear += () => ShowAchievement("SwampClear");
        Achievements.OnFirstVolcanoClear += () => ShowAchievement("VolcanoClear");
        Achievements.OnFirstLevelUp += () => UpdatePannel("LevelUp");
        Achievements.OnFirstDeath += () => UpdatePannel("Death");
        Achievements.OnFirstAbility += () => UpdatePannel("Ability");
        Achievements.OnFirstTradlear += () => UpdatePannel("Trade");
        Achievements.OnFirstCastleClear += () => UpdatePannel("CastleClear");
        Achievements.OnFirstSwampClear += () => UpdatePannel("SwampClear");
        Achievements.OnFirstVolcanoClear += () => UpdatePannel("VolcanoClear");

    }

    private void OnDisable()
    {
        // 이벤트 해제
        Achievements.OnFirstLevelUp -= () => ShowAchievement("LevelUp");
        Achievements.OnFirstDeath -= () => ShowAchievement("Death");
        Achievements.OnFirstAbility -= () => ShowAchievement("Ability");
        Achievements.OnFirstTradlear -= () => ShowAchievement("Trade");
        Achievements.OnFirstCastleClear -= () => ShowAchievement("CastleClear");
        Achievements.OnFirstSwampClear -= () => ShowAchievement("SwampClear");
        Achievements.OnFirstVolcanoClear -= () => ShowAchievement("VolcanoClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("LevelUp");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Death");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Ability");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("Trade");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("CastleClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("SwampClear");
        Achievements.OnFirstLevelUp -= () => UpdatePannel("VolcanoClear");
    }

    /// <summary>
    /// 도전과제 UI를 표시하는 메서드
    /// </summary>
    private void ShowAchievement(string achievementKey)
    {
        if (!achievements.ContainsKey(achievementKey))
        {
            Debug.LogWarning($"도전과제 데이터가 존재하지 않습니다: {achievementKey}");
            return;
        }

        AchievementData achievement = achievements[achievementKey];

        // UI 요소 업데이트
        achievementNameText.text = achievement.name;
        achievementDescriptionText.text = achievement.description;
        achievementImage.sprite = achievement.image;

        // UI 애니메이션 실행


        float targetY = achievementUI.transform.localPosition.y + 180f;
        float originalY = achievementUI.transform.localPosition.y; // 원래 위치 저장

        Sequence sequence = DOTween.Sequence();
        sequence.Append(achievementUI.transform.DOLocalMoveY(targetY, 0.5f).SetEase(Ease.OutCubic)) // 360 위로 이동
                .AppendInterval(1f) // 1초 대기
                .Append(achievementUI.transform.DOLocalMoveY(originalY, 0.5f).SetEase(Ease.InCubic)); // 다시 원래 위치로 이동
    }
    private void UpdatePannel(string achievementKey)
    {
        if (!achievements.ContainsKey(achievementKey))
        {
            Debug.LogWarning($"도전과제 데이터가 존재하지 않습니다: {achievementKey}");
            return;
        }
        switch (achievementKey)         
        {
            case "LevelUp":
                UpdateUIData(achievementKey, 0);
                break;
            case "Death":
                UpdateUIData(achievementKey, 1);
                break;
            case "Ability":
                UpdateUIData(achievementKey, 2);
                break;
            case "Trade":
                UpdateUIData(achievementKey, 3);
                break;
            case "CastleClear":
                UpdateUIData(achievementKey, 4);
                break;
            case "SwampClear":
                UpdateUIData(achievementKey, 5);
                break;
            case "VolcanoClear":
                UpdateUIData(achievementKey, 6);
                break;
        }
    }
    private void UpdateUIData(string achievementKey,int index)
    {
        AchievementData achievement = achievements[achievementKey];
        //achievementPanelName[index].text = achievement.name;
        achievementPanelDiscription[index].text = achievement.description;
        achievementPanelImage[index].sprite = achievement.image;
    }

}