using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    private GameObject hitBox;
    private EnemyAttack enemyAttack;
    public void SetUp(EnemyAttack enemyAttack)
    {
        this.enemyAttack = enemyAttack;
        this.hitBox = this.gameObject;
        DeactiveHitBox();
    }

    public void ActiveHitBox()
    {
        hitBox.SetActive(true);
    }
    public void DeactiveHitBox()
    {
        hitBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(enemyAttack.GetDamage());
            }
        }
    }
}
