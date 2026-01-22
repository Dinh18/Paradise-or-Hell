using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();

        playerMovement.Setup(rb,animator);
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
