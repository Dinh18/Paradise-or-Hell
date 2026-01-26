using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pick up " + collision.gameObject.name);
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
