using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Health Settings")]
    [SerializeField] private int currHealth = 100;
    private PlayerStats playerStats;
    private Animator animator;
    public static Action<int,int> OnHealthChange;

    public void Setup(Animator animator, PlayerStats playerStats)
    {
        this.animator = animator;
        this.playerStats = playerStats;
    }


    public void TakeDamage(int damage)
    {
        currHealth -= damage - playerStats.GetArmor();
        OnHealthChange?.Invoke(currHealth, playerStats.GetMaxHealth());
        animator.SetTrigger("Hurt");
    }

    public void RecoverHealth(int amount)
    {
        currHealth += Math.Min(amount, playerStats.GetMaxHealth() - currHealth);
        OnHealthChange?.Invoke(currHealth, playerStats.GetMaxHealth());
    }
}
