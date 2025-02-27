public class Blaze : AbilityBase
{
    private GameManager gameManager;
    private ProjectileManager projectileManager;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;

        projectileManager.SetBlaze(true);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = (isUpgraded ? abilityData.values[1] : abilityData.values[0]) * 0.01f;
        projectileManager.SetBlazeDecresaseDamage(value);
    }
}