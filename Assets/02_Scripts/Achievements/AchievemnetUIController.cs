using UnityEngine;

public class AchievementUIController : MonoBehaviour
{
    // Achievement UI의 RectTransform을 인스펙터에서 연결 (초기 Y값은 -180으로 설정)
    [SerializeField] private RectTransform achievementUI;

    private void OnEnable()
    {
        // 예시로 첫 라운드 클리어 이벤트가 발생하면 UI를 업데이트하도록 구독
        Achievements.OnFirstRoundClear += HandleFirstRoundClear;
    }

    private void OnDisable()
    {
        Achievements.OnFirstRoundClear -= HandleFirstRoundClear;
    }

    // 이벤트가 발생하면 Achievement UI의 Y값을 -180에서 180으로 변경
    private void HandleFirstRoundClear()
    {
        if (achievementUI != null)
        {
            Vector2 pos = achievementUI.anchoredPosition;
            pos.y = 180;
            achievementUI.anchoredPosition = pos;
            Debug.Log("Achievement UI의 Y값이 180으로 변경되었습니다.");
        }
    }
}