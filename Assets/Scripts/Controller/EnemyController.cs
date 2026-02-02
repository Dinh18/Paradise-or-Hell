using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private int index = 0;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;
    // void OnEnable()
    // {
    //     enemyHealth.ReSpawn();
    // }
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

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public int GetIndex() => index;

}
