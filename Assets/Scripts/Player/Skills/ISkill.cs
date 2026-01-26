using UnityEngine;

public abstract class ISkill : MonoBehaviour
{
    [Header("Skill Settings")]
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float coolDown = 1f;
    public int GetDamage()
    {
        return damage;
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    public abstract void Setup();
    public abstract void Attack();
}
