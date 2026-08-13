using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void CameraFollowPlayer()
    {
        transform.position = new Vector3 (player.position.x + 0, player.position.y + 0, -10);
    }
    
    void Update()
    {
        CameraFollowPlayer();
    }
}
