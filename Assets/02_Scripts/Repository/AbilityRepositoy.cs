using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class AbilityRepositoy : MonoBehaviour
{
    [SerializeField] private List<AbilityController> ownedAbilities = new List<AbilityController>();

    private Dictionary<AbilityEnum, GameObject> dicAbilityPrefabs = new Dictionary<AbilityEnum, GameObject>();
    private Dictionary<AbilityEnum, AbilityDataSO> dicAbilityDataSO = new Dictionary<AbilityEnum, AbilityDataSO>();

    [SerializeField] private AbilityDataSO[] abilityDataSOs;
    [SerializeField] private GameObject[] abilityPrefabs;

    private void Awake()
    {
        InitDictionary();
    }

    /// <summary>
    /// abilityDataSOs 변수 세팅
    /// </summary>
    /// <param name="abilityDataSOs"></param>
    public void SetabilityDataSOs(AbilityDataSO[] abilityDataSOs)
    {
        this.abilityDataSOs = abilityDataSOs;
    }

    /// <summary>
    /// dicAbilityPrefabs, dicAbilityDataSO초기화
    /// </summary>
    private void InitDictionary()
    {
        dicAbilityPrefabs.Clear();
        dicAbilityDataSO.Clear();

        for (int i = 0; i < abilityDataSOs.Length; i++)
        {
            AbilityEnum key = abilityDataSOs[i].Ability;
            GameObject prefab = abilityPrefabs[i];
            AbilityDataSO data = abilityDataSOs[i];

            dicAbilityPrefabs[key] = prefab;
            dicAbilityDataSO[key] = data;
        }
    }

    /// <summary>
    /// 소유중인 모든 어빌리티 소멸
    /// </summary>
    public void ClearOwnedAbilities()
    {
        for (int i = ownedAbilities.Count - 1; i >= 0; i--)
        {
            Destroy(ownedAbilities[i].gameObject);
            ownedAbilities.RemoveAt(i); 
        }
    }

    /// <summary>
    /// 해당 어빌리티를 Dic에서 검색 및 오브젝트 생성하여 소유 어빌리티 목록에 추가 또는 강화
    /// </summary>
    /// <param name="ability">어빌리티의 ID</param>
    /// <returns>생성된 어빌리티 오브젝트</returns>
    public GameObject SetAbility(AbilityEnum ability)
    {
        // 이미 소유 중인 경우 강화
        foreach (var varAbilityController in ownedAbilities)
        {
            if (varAbilityController.AbilityBase != null && varAbilityController.AbilityBase.abilityData.abilityID == ability)
            {
                UpgradeOwnedAbility(ability);
                return null;
            }
        }

        GameObject abilityPrefab = Instantiate(dicAbilityPrefabs[ability]);
        AbilityController abilityController = abilityPrefab.transform.GetComponent<AbilityController>();

        ownedAbilities.Add(abilityController);
        abilityController.Init(dicAbilityDataSO[ability]);

        return abilityPrefab;
    }

    /// <summary>
    /// 리스트 복사 없이 ReadOnlyCollection 반환, 수정 불가
    /// </summary>
    public ReadOnlyCollection<AbilityController> GetOwnedAbilities()
    {
        return ownedAbilities.AsReadOnly();
    }

    /// <summary>
    /// 소유중인 어빌리티 중의 해당 어빌리티 강화
    /// </summary>
    /// <param name="ability"></param>
    public void UpgradeOwnedAbility(AbilityEnum ability)
    {
        foreach (var abilityController in ownedAbilities)
        {
            if (abilityController.AbilityBase.abilityData.abilityID == ability)
            {
                abilityController.AbilityBase.UpgradeAbility();
                return;
            }
        }
    }


    /// <summary>
    /// 모든 어빌리티 중 원하는 어빌리티의 AbilityData 찾아서 반환하는 함수
    /// </summary>
    /// <param name="abilityEnum">원하는 어빌리티의 ID</param>
    /// <returns></returns>
    public AbilityDataSO FindAbilityData(AbilityEnum abilityEnum)
    {
        return dicAbilityDataSO[abilityEnum];
    }
}
