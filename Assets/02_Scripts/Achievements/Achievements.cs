using System;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    // 각 도전과제의 상태 (static으로 선언하여 전역 상태로 관리)
    public static bool isFirstLevelUp { get; private set; } = false;
    public static bool isFirstDeath { get; private set; } = false;
    public static bool isFirstAbility { get; private set; } = false;
    public static bool isFirstTradlear { get; private set; } = false;
    public static bool isFirstCastleClear { get; private set; } = false;
    public static bool isFirstSwampClear { get; private set; } = false;
    public static bool isFirstVolcanoClear { get; private set; } = false;

    // 단발성 이벤트 (각 도전과제 발생 시 한 번만 호출)
    public static event Action OnFirstLevelUp;
    public static event Action OnFirstDeath;
    public static event Action OnFirstAbility;//추가완료
    public static event Action OnFirstTradlear;
    public static event Action OnFirstCastleClear;
    public static event Action OnFirstSwampClear;
    public static event Action OnFirstVolcanoClear;

 
    
    /// <summary>
    /// 도전과제 트리거 메서드들
    /// </summary>
    public static void TriggerFirstLevelUp()
    {
        if (!isFirstLevelUp)
        {
            isFirstLevelUp = true;
            OnFirstLevelUp?.Invoke();
        }
    }

    public static void TriggerFirstDeath()
    {
        if (!isFirstDeath)
        {
            isFirstDeath = true;
            OnFirstDeath?.Invoke();
        }
    }


    public static void TriggerFirstAbility()
    {
        if (!isFirstAbility)
        {
            isFirstAbility = true;
            OnFirstAbility?.Invoke();
        }
    }

    public static void TriggerFirstTradlear()
    {
        if (!isFirstTradlear)
        {
            isFirstTradlear = true;
            OnFirstTradlear?.Invoke();
        }
    }

    public static void TriggerFirstCastleClear()
    {
        if (!isFirstCastleClear)
        {
            isFirstCastleClear = true;
            OnFirstCastleClear?.Invoke();
        }
    }

    public static void TriggerFirstSwampClear()
    {
        if (!isFirstSwampClear)
        {
            isFirstSwampClear = true;
            OnFirstSwampClear?.Invoke();
        }
    }

    public static void TriggerFirstVolcanoClear()
    {
        if (!isFirstVolcanoClear)
        {
            isFirstVolcanoClear = true;
            OnFirstVolcanoClear?.Invoke();
        }
    }
}
