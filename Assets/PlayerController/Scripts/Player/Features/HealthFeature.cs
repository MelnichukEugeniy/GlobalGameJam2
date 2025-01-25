using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(HealthFeature), menuName = "Player/Features/Health")]
public class HealthFeature : BasePlayerFeature
{
    public float Health { get; private set; }
    public float MaxHealth => config.MaxHealth;
    public float HealthPercentage => Health / MaxHealth;
    
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
        if(Math.Abs(Health - MaxHealth) < .001f)
            return;

        restoreValue = Mathf.Clamp(restoreValue, 0, restoreValue);
        if(restoreValue == 0)
            return;

        Health += restoreValue;
        if (Health > MaxHealth)
            Health = MaxHealth;
        
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

#if UNITY_EDITOR
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            TakeDamage(5);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            RestoreHealth(5);
        }
    }
#endif

    private void Reset()
    {
        IsDead = false;
        Health = config.BaseHealth;
    }
}