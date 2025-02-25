using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AbilityRepositoy abilityRepositoy;
    public AbilityRepositoy AbilityRepositoy => abilityRepositoy;

    [SerializeField] private Transform abilityParent;

    private void Awake()
    {
        abilityParent = transform;
    }

    /// <summary>
    /// 소유중인 모든 어빌리티 소멸
    /// </summary>
    public void ClearOwnedAbilities()
    {
        AbilityRepositoy.ClearOwnedAbilities();
    }

    /// <summary>
    /// 해당 어빌리티 획득 및 오브젝트 생성하여 지정된 객체 하위로 생성
    /// </summary>
    /// <param name="abilityEnum">어빌리티의 ID</param>
    public void SetAbility(AbilityEnum abilityEnum)
    {
        Debug.Log($"{abilityEnum}받앗다");
        AbilityRepositoy.SetAbility(abilityEnum).transform.SetParent(abilityParent);
    }

    /// <summary>
    /// 소유중인 어빌리티 사용
    /// </summary>
    public void UseAbility()
    {
        foreach (AbilityController ability in AbilityRepositoy.GetOwnedAbilities())
        {
            ability.UseSkill();
        }
    }

    /// <summary>
    /// 소유중인 어빌리티 중에서 원하는 어빌리티 강화
    /// </summary>
    /// <param name="abilityEnum">원하는 어빌리티의 ID</param>
    public void UpgradeOwnedAbility(AbilityEnum abilityEnum)
    {
        AbilityRepositoy.UpgradeOwnedAbility(abilityEnum);
    }

    /// <summary>
    /// 모든 어빌리티 중 원하는 어빌리티의 AbilityData 찾아서 반환하는 함수
    /// </summary>
    /// <param name="abilityEnum">원하는 어빌리티의 ID</param>
    /// <returns></returns>
    public AbilityData FindAbilityData(AbilityEnum abilityEnum)
    {
        return abilityRepositoy.FindAbilityData(abilityEnum);
    }
}
