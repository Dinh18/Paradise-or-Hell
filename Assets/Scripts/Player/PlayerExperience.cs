using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int expToUpLevel = 100;
    [SerializeField] private int currExp = 0;
    private PlayerStats playerStats;
    public void Setup(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }
    public void AddExperience(int amount)
    {
        currExp += (int)(amount * playerStats.GetGrowthExperience());
        if(currExp >= expToUpLevel)
        {
            currExp = currExp - expToUpLevel;
            expToUpLevel += 50;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        UpgradeController.instance.ShowUpgradePanel();
    }
}
