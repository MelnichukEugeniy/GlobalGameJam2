using UnityEngine;

public class HealthFeature : BasePlayerFeature
{
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float HealthPercentage
    {
        get => Mathf.InverseLerp(0, MaxHealth, Health);
    }
    
    public bool IsDead { get; private set; }
    
    [SerializeField]
    private PlayerConfig config;

    public override void InitializeWithPlayer(PlayerController player)
    {
        base.InitializeWithPlayer(player);

        IsDead = false;
        Health = config.BaseHealth;
        InvokePlayerHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, damage);
        if(damage == 0)
            return;

        Health -= damage;
        if (IsPlayerDead())
        {
            Health = 0;
            IsDead = true;
            EventBus<PlayerDeadEvent>.Raise(new PlayerDeadEvent(playerController));
        }

        InvokePlayerHealthChanged();
    }

    public void RestoreHealth(float restoreValue)
    {
        restoreValue = Mathf.Clamp(restoreValue, 0, restoreValue);
        if(restoreValue == 0)
            return;

        Health += restoreValue;
        InvokePlayerHealthChanged();
    }

    private bool IsPlayerDead()
    {
        return Health <= 0;
    }

    private void InvokePlayerHealthChanged()
    {
        EventBus<PlayerHealthChangedEvent>.Raise(new PlayerHealthChangedEvent(Health, this, playerController));
    }
}