using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float countDownTime = 3f;
    [SerializeField] private int damage = 10;
    private EnemyHitBox enemyHitBox;
    private float currTime = 0f;
    private int animID_Attack;

    public void Setup()
    {
        animID_Attack = Animator.StringToHash("Attack");
        enemyHitBox = GetComponentInChildren<EnemyHitBox>();

        enemyHitBox.SetUp(this);
    }

    public void Attack(Animator animator, bool canAttack)
    {
        if(currTime < countDownTime) currTime += Time.deltaTime;
        if (currTime >= countDownTime && canAttack)
        {
            enemyHitBox.ActiveHitBox();
            // Thực hiện tấn công
            currTime = 0f;
            animator.SetTrigger(animID_Attack);
        }
    }

    public int GetDamage() => damage;
}
