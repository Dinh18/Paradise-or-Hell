using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instant;
    // [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Joystick joystick;
    private PlayerStats playerStats;
    private Animator animator;
    private int animID_InputX;
    private int animID_InputY;
    private int animID_Speed;
    private int animID_IsHurt;

    public void Setup(Rigidbody2D rb, Animator animator, PlayerStats playerStats)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStats = playerStats;
        instant = this;

        animID_InputX = Animator.StringToHash("InputX");
        animID_InputY = Animator.StringToHash("InputY");
        animID_Speed = Animator.StringToHash("Speed");
        animID_IsHurt = Animator.StringToHash("IsHurt");
    }

    public void HandleInput()
    {
        // Kết hợp cả Joystick (Mobile)
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        movement = new Vector2(x, y);

        // Giới hạn độ dài vector để đi chéo không bị nhanh hơn đi thẳng
        movement = Vector2.ClampMagnitude(movement, 1f);

        // Test Hurt (Chỉ dùng khi debug)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(animID_IsHurt);
        }
    }

    public void UpdateAnimation()
    {
        if(animator == null) return;
        // Cập nhật tốc độ (để chuyển từ Idle -> Run)
        animator.SetFloat(animID_Speed, movement.sqrMagnitude);

        // Chỉ cập nhật hướng (InputX, InputY) khi nhân vật thực sự di chuyển
        // Giúp nhân vật giữ nguyên hướng mặt khi dừng lại
        if (movement.sqrMagnitude > 0.01f)
        {
            animator.SetFloat(animID_InputX, movement.x);
            animator.SetFloat(animID_InputY, movement.y);
        }
    }

    public void ApplyMovement()
    {
        // Di chuyển vật lý
        if(rb !=null ) rb.MovePosition(rb.position + movement * playerStats.GetSpeed() * Time.fixedDeltaTime);
    }

    public Vector3 playerPosition()
    {
        return this.gameObject.transform.position;
    }

}
