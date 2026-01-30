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
        ISkill skillToUpgrade = GetSkill(skillName);

        if(skillToUpgrade == null) return;

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

    public void AddSkill(string skillName, GameObject skillPrefab)
    {
        ISkill skillToUpgrade = GetSkill(skillName);
        if(skillToUpgrade == null)
        {
            GameObject skillObj = GameObject.Instantiate(skillPrefab, this.transform);
            ISkill newSkill = skillObj.GetComponent<ISkill>();
            newSkill.Setup(
                GetComponent<PlayerStats>(),
                GetComponent<PlayerHealth>()
            );
            skills.Add(newSkill);
        }
        else
        {
            skillToUpgrade.UpgradeAmount();
        }

    }

    public ISkill GetSkill(string skillName)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if(skills[i].GetSkillName() == skillName)
            {
                return skills[i];
            }
        }
        return null;
    }
}
