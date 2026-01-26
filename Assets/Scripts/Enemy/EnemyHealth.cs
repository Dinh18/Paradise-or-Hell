using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private GameObject expGemPrefab;
    private Animator animator;

    public void Setup(Animator animator)
    {
        this.animator = animator;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        expGemPrefab = Instantiate(expGemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
