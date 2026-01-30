using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public static PlayerSkills instant;
    private List<ISkill> skills;
    private int currTime = 0;
    public void Setup(PlayerStats playerStats, PlayerHealth playerHealth)
    {
        instant = this;
        skills = new List<ISkill>();
        ISkill[] skillArray = GetComponentsInChildren<ISkill>();
        for(int i = 0; i < skillArray.Length; i++)
        {
            skillArray[i].Setup(playerStats, playerHealth);
            skills.Add(skillArray[i]);
        }
    }

    public void ExecuteSkills()
    {
        for(int i = 0; i < skills.Count; i++)
        {
            skills[i].Attack();
        }
    }

    public void UpgradeStat(string skillName, StatSkillType statSkillType, float amount)
    {
        ISkill skillToUpgrade = null;
        for(int i = 0; i < skills.Count; i++)
        {
            if(skills[i].GetSkillName() == skillName)
            {
                skillToUpgrade = skills[i];
                break;
            }
        }

        switch(statSkillType)
        {
            case StatSkillType.CoolDown:
                skillToUpgrade.UpgradeCoolDown(amount);
                break;
            case StatSkillType.Damage:
                skillToUpgrade.UpgradeDamage((int)amount);
                break;
        }
    }
}
