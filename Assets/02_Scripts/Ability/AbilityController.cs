using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private AbilityBase abilityBase;

    public AbilityBase AbilityBase { get; private set; }

    /// <summary>
    /// 어빌리티 최초 실행 함수 작동 및 초기화
    /// </summary>
    /// <param name="abilityDataSO"></param>
    public void Init(AbilityDataSO abilityDataSO)
    {
        if (abilityBase == null)
        {
            return;
        }

        AbilityBase = abilityBase;

        AbilityBase?.Init(abilityDataSO);
    }


    /// <summary>
    /// 현재 컨트롤러가 가지고 있는 Abilitybase의 UseSkill() 작동
    /// </summary>
    public void UseSkill()
    {
        AbilityBase?.UseSkill();
    }

}
