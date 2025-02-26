using UnityEngine;

public class AchievementUIController : MonoBehaviour
{
    // Achievement UI의 RectTransform을 인스펙터에서 연결 (초기 Y값은 -180으로 설정)
    [SerializeField] private RectTransform achievementUI;

    private void OnEnable()
    {
        Achievements.OnFirstLevelUp += GetAchievemeent;
        Achievements.OnFirstDeath += GetAchievemeent;
        Achievements.OnFirstRoundClear += GetAchievemeent;
        Achievements.OnFirstAbility += GetAchievemeent;
        Achievements.OnFirstTradlear += GetAchievemeent;
        Achievements.OnFirstCastleClear += GetAchievemeent;
        Achievements.OnFirstSwampClear += GetAchievemeent;
        Achievements.OnFirstVolcanoClear += GetAchievemeent;
}

    private void OnDisable()
    {
        Achievements.OnFirstRoundClear -= GetAchievemeent;

    }

    // 이벤트가 발생하면 Achievement UI의 Y값을 -180에서 180으로 변경
    private void GetAchievemeent()
    {

    }
}