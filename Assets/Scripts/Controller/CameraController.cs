using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(PlayerMovement.instant.playerPosition().x, 
                                                    PlayerMovement.instant.playerPosition().y, 
                                                    mainCamera.transform.position.z);
    }
}
