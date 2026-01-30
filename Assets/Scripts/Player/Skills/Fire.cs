using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : ISkill
{
    [Header("Fire Skill Specific")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float fireForce = 2f;
    // [SerializeField] private int numberOfFires = 1;
    private PlayerStats playerStats;
    private PlayerHealth playerHealth;

    private float currTime = 0;
    public override void Attack()
    {
        currTime += Time.deltaTime;
        if(currTime >= coolDown)
        { 
            for(int i = 0; i < amount; i++)
            {
                StartCoroutine(SpawnFireAfterDelay(0.1f + (i * 0.1f), i));
            }
            currTime = 0;
        }
    }

    private void SpawnFire(int i)
    {
        GameObject fire = GameObject.Instantiate(firePrefab, this.transform.position, Quaternion.identity);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        SkillHitBox skillHitBox = fire.GetComponent<SkillHitBox>();
        if(skillHitBox != null) skillHitBox.SetUp(this, playerHealth, playerStats);
        Vector2 dicrection = (PlayerMovement.instant.GetMovement() == Vector2.zero) ? Vector2.right : PlayerMovement.instant.GetMovement();
        // dicrection.x += i * 0.2f;
        // transform.right = dicrection;
        dicrection.Normalize();
        float agle = Mathf.Atan2(dicrection.y, dicrection.x) * Mathf.Rad2Deg;
        fire.transform.rotation = Quaternion.Euler(new Vector3(0, 0, agle));
        rb.AddForce(dicrection * fireForce, ForceMode2D.Impulse);
    }

    private IEnumerator SpawnFireAfterDelay(float delay, int i)
    {
        yield return new WaitForSeconds(delay);
        SpawnFire(i);
    }

    public override void Setup(PlayerStats playerStats, PlayerHealth playerHealth)
    {
        this.playerStats = playerStats;
        this.playerHealth = playerHealth;
    }

}
