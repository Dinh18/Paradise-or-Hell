using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int expToUpLevel = 100;
    [SerializeField] private int currExp = 0;
    public void AddExperience(int amount)
    {
        currExp += amount;
        if(currExp >= expToUpLevel)
        {
            currExp = currExp - expToUpLevel;
            expToUpLevel += 50;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        
    }
}
