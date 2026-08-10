using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        //Vector2 targetDirection = EnemyController.Instance.transform.position - transform.position;
        
        //GameObject newBullet = Instantiate(bulletPrefab, targetDirection, Quaternion.identity);
        //newBullet.transform.right = targetDirection.normalized;
    }
}
