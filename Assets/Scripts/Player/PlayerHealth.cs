using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currHealth = 100;
    private Animator animator;

    public void Setup(Animator animator)
    {
        this.animator = animator;
    }


    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        animator.SetTrigger("Hurt");
    }
}
