using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private List<ISkill> skills;
    private int currTime = 0;
    public void Setup()
    {
        skills = new List<ISkill>();
        ISkill[] skillArray = GetComponentsInChildren<ISkill>();
        for(int i = 0; i < skillArray.Length; i++)
        {
            skillArray[i].Setup();
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
}
