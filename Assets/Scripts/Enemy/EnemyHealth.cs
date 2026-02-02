using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject expGemPrefab;
    // private EnemyController enemyController;
    private Animator animator;
    public static Action<GameObject> OnEnemyKilled;

    public void Setup(Animator animator)
    {
        this.animator = animator;
        currentHealth = maxHealth;
    }

    public void ReSpawn()
    {
        currentHealth = maxHealth;
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
        // GameObject gemObj = Instantiate(expGemPrefab, transform.position, Quaternion.identity);
        // currentHealth = maxHealth;
        // this.gameObject.SetActive(false);
        OnEnemyKilled?.Invoke(this.gameObject);
    }

    public GameObject GetExpGemPrefab() => expGemPrefab;
}
