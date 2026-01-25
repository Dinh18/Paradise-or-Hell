using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed = 5f;
    [SerializeField]private float minDistanceToPlayer = 0.5f;
    
    private Vector2 dir;
    private Vector2 movement;
    private bool shouldMove = true;
    private float minDistanceSqr;
    private float distSqr;

    private int animID_DirX;
    private int animID_DirY;
    private int animID_Speed;

    public void Setup()
    {

        animID_DirX = Animator.StringToHash("DirX");
        animID_DirY = Animator.StringToHash("DirY");
        animID_Speed = Animator.StringToHash("Speed");

        minDistanceSqr = minDistanceToPlayer * minDistanceToPlayer;
    }
    // Tìm đường đến Player
    public void FindPathToPlayer(Rigidbody2D rb, Animator animator)
    {
        // Nếu không có Player thì không di chuyển
        if (PlayerMovement.instant == null)
        {
            dir = Vector2.zero;
            shouldMove = false;
            UpdateAnimation(animator);
            return; 
        }

        Vector3 targetPos = PlayerMovement.instant.playerPosition(); // Lấy vị trí Player

        distSqr = ((Vector2)targetPos - rb.position).sqrMagnitude;  // Tính khoảng cách bình phương đến Player

        if (shouldMove) // Chỉ di chuyển khi được phép
        {
            dir = ((Vector2)targetPos - rb.position).normalized; // Tính hướng di chuyển về phía Player
        }
        movement = (distSqr > minDistanceSqr) ? dir : Vector2.zero; // Dừng lại nếu quá gần Player
        UpdateAnimation(animator);
    }
    // Di chuyển đến Player
    public void ApplyMovement(Rigidbody2D rb)
    {
        rb.linearVelocity = movement * speed;
    }
    // Cập nhật animation
    void UpdateAnimation(Animator animator)
    {
        animator.SetFloat(animID_DirX, dir.x);
        animator.SetFloat(animID_DirY, dir.y);

        animator.SetFloat(animID_Speed, shouldMove ? movement.sqrMagnitude : 0f);
    }

    public bool CanAttack()
    {
        return distSqr < minDistanceSqr;
    }
}
