using UnityEngine;

public class SkillHitBox : MonoBehaviour
{
    private GameObject hitbox;
    private ISkill skill;
    public void SetUp(ISkill skill)
    {
        this.skill = skill;
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
