using UnityEngine;

public class SkillHitBox : MonoBehaviour
{
    private GameObject hitbox;
    private ISkill skill;
    private PlayerHealth playerHealth;
    private PlayerStats playerStats;

    public void SetUp(ISkill skill, PlayerHealth playerHealth, PlayerStats playerStats)
    {
        this.skill = skill;
        this.playerHealth = playerHealth;
        this.playerStats = playerStats;
        this.hitbox = this.gameObject;
        DeactiveHitBox();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if(enemyHealth != null) 
            {
                enemyHealth.TakeDamage(skill.GetDamage());
                playerHealth.RecoverHealth((int)(skill.GetDamage() * playerStats.GetRecoverHealth()));
            }
        }
    }

    public void ActiveHitBox()
    {
        hitbox.SetActive(true);
    }
    public void DeactiveHitBox()
    {
        hitbox.SetActive(false);
    }
}
