using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();

        enemyMovement.Setup();
        enemyAttack.Setup();
        enemyHealth.Setup(animator);
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement.FindPathToPlayer(rb, animator);
        enemyAttack.Attack(animator,enemyMovement.CanAttack());
    }
    void FixedUpdate()
    {
        enemyMovement.ApplyMovement(rb);
    }

}
