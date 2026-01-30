using UnityEngine;

public class Whip : ISkill
{
    [SerializeField]private int numberOfWhips = 1;
    private SkillHitBox[] skillHitBoxes;
    private float currTime = 0;

    public override void Setup(PlayerStats playerStats, PlayerHealth playerHealth)
    {
        skillHitBoxes = GetComponentsInChildren<SkillHitBox>();
        for(int i = 0; i < skillHitBoxes.Length; i++)
        {
            skillHitBoxes[i].SetUp(this, playerHealth, playerStats);
        }
    }

    public override void Attack()
    {
        currTime += Time.deltaTime;
        if(currTime >= coolDown)
        { 
            for(int i = 0; i < numberOfWhips; i++)
            {
                skillHitBoxes[i].ActiveHitBox();
                currTime = 0;
            }
        }
    }
}
