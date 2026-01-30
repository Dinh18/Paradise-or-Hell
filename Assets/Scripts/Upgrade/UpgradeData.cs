using UnityEngine;
public enum UpgradeType { StatUpgrade, NewWeapon }
public enum StatType
{
    MaxHealth,
    Speed,
    Armor,
    PickUpArea,
    GrowthExperience,
    RecoverHealth
}

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Game/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade Information")]
    [SerializeField] private string upgradeName;
    [SerializeField][TextArea] private string description;
    [Header("Upgrade Effects")]
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private StatType statName;
    [SerializeField] private float amount;
    [SerializeField] private GameObject weaponPrefab;

    public string GetUpgradeName() => upgradeName;
    public string GetDescription() => description;
    public UpgradeType GetUpgradeType() => upgradeType;
    public StatType GetStat() => statName;
    public float GetAmount() => amount;
    public GameObject GetWeaponPrefab() => weaponPrefab;

}
