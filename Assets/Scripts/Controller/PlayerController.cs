using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instant;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerSkills playerSkills;
    private PlayerExperience playerExperience;
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerStats playerStats;
    void Awake()
    {
        instant = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerSkills = GetComponent<PlayerSkills>();
        playerExperience = GetComponent<PlayerExperience>();
        playerStats = GetComponent<PlayerStats>();


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();
            
        playerSkills.Setup(playerStats, playerHealth);
        playerMovement.Setup(rb,animator,playerStats);
        playerHealth.Setup(animator, playerStats);
        playerExperience.Setup(playerStats);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.HandleInput();

        playerMovement.UpdateAnimation();
        
        playerSkills.ExecuteSkills();
    }

    void FixedUpdate()
    {
        playerMovement.ApplyMovement();
    }

    public Vector2 PlayerPosition()
    {
        return transform.position;
    }
}
