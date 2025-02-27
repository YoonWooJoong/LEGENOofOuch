public class AttackBoost : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;

        if (isUpgraded)
        {
            player.AtkBuf -= abilityData.values[0] * 0.01f;
        }

        float attackBoost = (isUpgraded ? abilityData.values[1] : abilityData.values[0]) * 0.01f;
        player.AtkBuf += attackBoost;
    }
}