using UnityEngine;

public class ExperienceGem : IItem
{
    [SerializeField] int amount = 10;
    public override void Pickup(GameObject player)
    {
        PlayerExperience playerExperience = player.GetComponent<PlayerExperience>();
        if(playerExperience != null)
        {
            playerExperience.AddExperience(amount); 
            this.gameObject.SetActive(false);
        }
    }
}
