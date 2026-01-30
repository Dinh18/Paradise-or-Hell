using UnityEngine;
public enum UpgradeType
{
    StatUpgrade,
    NewWeapon,
    StatWeaponUpgrade,
    None 
}
public enum StatType
{
    MaxHealth,
    Speed,
    Armor,
    PickUpArea,
    GrowthExperience,
    RecoverHealth,
    None
}

public enum StatSkillType
{
    Damage,
    CoolDown,
    None
}

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Game/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade Information")]
    [SerializeField] private string upgradeName;
    [SerializeField][TextArea] private string description;
    [Header("Upgrade Player Stats")]
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private StatType statName;
    [SerializeField] private float amount;
    [Header("Upgrade Skill")] 
    [SerializeField] private StatSkillType statSkillType;
    [SerializeField] private GameObject weaponPrefab;

    public string GetUpgradeName() => upgradeName;
    public string GetDescription() => description;
    public UpgradeType GetUpgradeType() => upgradeType;
    public StatType GetStat() => statName;
    public StatSkillType GetStatSkillType() => statSkillType;
    public float GetAmount() => amount;
    public GameObject GetWeaponPrefab() => weaponPrefab;

}
