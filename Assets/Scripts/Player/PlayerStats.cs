using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    [Header("Player Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private int armor;
    [SerializeField] private float pickUpArea;
    [SerializeField] private float growthExperience;
    [SerializeField] private float recoverHealth;

    void Awake()
    {
        instance = this;
    }

    public void IncreaseStat(StatType statType, float amount)
    {
        switch(statType)
        {
            case StatType.MaxHealth:
                maxHealth += (int)(maxHealth * amount);
                break;
            case StatType.Speed:
                speed += (int)(speed * amount);
                break;
            case StatType.Armor:
                armor += (int)amount;
                break;
            case StatType.PickUpArea:
                pickUpArea += amount * amount;
                break;
            case StatType.GrowthExperience:
                growthExperience += growthExperience * amount;
                break;
            case StatType.RecoverHealth:
                recoverHealth += amount;
                break;
            default:
                Debug.Log("Stat type not recognized.");
                break;
        }
    }

    public int GetMaxHealth() => maxHealth;
    public float GetSpeed() => speed;
    public int GetArmor() => armor;
    public float GetPickUpArea() => pickUpArea;
    public float GetGrowthExperience() => growthExperience;
    public float GetRecoverHealth() => recoverHealth;
}
