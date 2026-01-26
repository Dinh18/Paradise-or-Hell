
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed = 5f;
    [SerializeField]private float minDistanceToPlayer = 0.5f;
    [SerializeField] private float separationRadius = 0.5f; // Bán kính vòng tròn cá nhân
    [SerializeField] private float separationForce = 2f;    // Lực đẩy ra
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float turnSpeed = 5f; // Tốc độ quay đầu (Càng nhỏ càng mượt/trượt)
    // private Vector2 currentDir; // Hướng hiện tại (để làm mượt)
    private Vector2 moveDirSmooth; // Dùng cho Rigidbody (để đi)
    private Vector2 lookDir;       // Dùng cho Animator (để nhìn)
    // private Vector2 dir;
    // private Vector2 movement;
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
            // dir = Vector2.zero;
            UpdateAnimation(animator);
            return; 
        }

        Vector3 targetPos = PlayerMovement.instant.playerPosition(); // Lấy vị trí Player
        Vector2 vectorToPlayer = (Vector2)targetPos - rb.position;
        distSqr = ((Vector2)targetPos - rb.position).sqrMagnitude;  // Tính khoảng cách bình phương đến Player

        if (vectorToPlayer.sqrMagnitude > 0.01f)
        {
            lookDir = vectorToPlayer.normalized;
        }
        // Cập nhật Animator ngay lập tức bằng lookDir (không dùng moveDir)
        UpdateAnimation(animator);

        // 1. Tính hướng mong muốn (Target Direction)
        Vector2 targetMoveDir = Vector2.zero;

        // A. Lực đuổi theo (chỉ khi ở xa)
        if (distSqr > minDistanceSqr)
        {
            targetMoveDir = vectorToPlayer.normalized;
        }

        // Lực tách đàn (Separation)
        Vector2 separation = GetSeparationVector();
        targetMoveDir += separation * separationForce;

        // 3. Cộng gộp lực
        // Logic: (Lực đuổi) + (Lực đẩy * Hệ số)
        // 2. --- KHỬ GIẬT (SMOOTHING) ---
        // Thay vì gán ngay lập tức, ta xoay hướng từ từ
        if (targetMoveDir.sqrMagnitude > 0.01f)
        {
            targetMoveDir.Normalize();
            moveDirSmooth = Vector2.Lerp(moveDirSmooth, targetMoveDir, turnSpeed * Time.deltaTime);
        }
        else
        {
            // Nếu không cần đi đâu cả, giảm tốc từ từ về 0
            moveDirSmooth = Vector2.Lerp(moveDirSmooth, Vector2.zero, turnSpeed * Time.deltaTime);
        }
    }
    // Di chuyển đến Player
    public void ApplyMovement(Rigidbody2D rb)
    {
        rb.linearVelocity = moveDirSmooth * speed;
    }
    // Cập nhật animation
    void UpdateAnimation(Animator animator)
    {
        animator.SetFloat(animID_DirX, lookDir.x);
        animator.SetFloat(animID_DirY, lookDir.y);

        animator.SetFloat(animID_Speed, moveDirSmooth.sqrMagnitude);
    }

    public bool CanAttack()
    {
        return distSqr < minDistanceSqr;
    }

    private Vector2 GetSeparationVector()
    {
        Vector2 separateVector = Vector2.zero;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, separationRadius, enemyLayer);

        foreach (var hit in hitColliders)
        {
            if (hit.gameObject != gameObject)
            {
                Vector2 difference = (Vector2)transform.position - (Vector2)hit.transform.position;

                if (difference.sqrMagnitude <= 0.001f)
                {
                    difference = new Vector2(GetInstanceID() % 2 == 0 ? 1 : -1, 0); 
                }
                separateVector += difference.normalized;
            }
        }
        return separateVector;
    }
}
