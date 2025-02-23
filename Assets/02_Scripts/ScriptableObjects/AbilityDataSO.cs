using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.Rendering.DebugUI;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptable Object/Ability Data", order = int.MaxValue)]
public class AbilityDataSO : ScriptableObject
{
    [SerializeField] private AbilityEnum ability;
    [SerializeField] private string abilityName;
    [SerializeField] private string description;
    [SerializeField] private RankEnum rank;
    [SerializeField] private bool canUpgrade;
    [SerializeField] private float[] values;

    public AbilityEnum Ability => ability;
    public string AbilityName => abilityName;
    public string Description => description;
    public RankEnum Rank => rank;
    public bool CanUpgrade => canUpgrade;
    public float[] Values => values;

    public void SetData(AbilityEnum abilityEnum, string name, string desc, RankEnum rankEnum, bool canUpgrade, float[] values)
    {
        this.ability = abilityEnum;
        this.abilityName = name;
        this.description = desc;
        this.rank = rankEnum;
        this.canUpgrade = canUpgrade;
        this.values = values;
    }
}