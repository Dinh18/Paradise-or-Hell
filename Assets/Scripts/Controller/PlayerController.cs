using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();

        playerMovement.Setup(rb,animator);
        playerHealth.Setup(animator);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.HandleInput();
        playerMovement.UpdateAnimation();
    }

    void FixedUpdate()
    {
        playerMovement.ApplyMovement();
    }
}
