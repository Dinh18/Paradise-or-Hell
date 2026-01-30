using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Item"))
        {
            IItem item = collision.gameObject.GetComponent<IItem>();
            if(item != null)
            {
                item.Pickup(this.transform.parent.gameObject);
            }
        }
    }
}
