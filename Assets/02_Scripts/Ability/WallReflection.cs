public class WallReflection : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        GameManager.Instance.ProjectileManager.SetContactWallCount(2);

        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];

        value = (100 - value) * 0.01f;
        GameManager.Instance.ProjectileManager.SetContactWallDecreaseDamage(value);
    }
}