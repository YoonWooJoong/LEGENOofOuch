using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptable Object/Ability Data", order = int.MaxValue)]
public class AbilityDataSO : ScriptableObject
{
    [SerializeField] private AbilityEnum ability;
    [SerializeField] private string abilityName;
    [SerializeField] private string description;
    [SerializeField] private RankEnum rank;
    [SerializeField] private float[] values;

    public AbilityEnum Ability => ability;
    public string AbilityName => abilityName;
    public string Description => description;
    public RankEnum Rank => rank;
    public float[] Values => values;
}