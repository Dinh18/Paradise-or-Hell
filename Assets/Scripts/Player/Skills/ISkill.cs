using UnityEngine;
using UnityEngine.UI;

public abstract class ISkill : MonoBehaviour
{
    [Header("Skill Settings")]
    [SerializeField] protected string skillName = "Weapon";
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float coolDown = 1f;
    [SerializeField] protected int amount = 1;
    [SerializeField] protected int maxAmount = 5;
    public int GetDamage()
    {
        return damage;
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    public abstract void Setup(PlayerStats playerStats, PlayerHealth playerHealth);
    public abstract void Attack();
    public string GetSkillName()
    {
        return skillName;
    }
    public void UpgradeDamage(int amount)
    {
        damage += amount;
    }
    public void UpgradeCoolDown(float amount)
    {
        coolDown -= coolDown * amount;
        if(coolDown < 0.1f)
        {
            coolDown = 0.1f;
        }
    }

    public void UpgradeAmount()
    {
        if(amount >= maxAmount)
        {
            damage += (int) damage/maxAmount;
        }
        else
        {
            amount+=1;
        }
    }
}
