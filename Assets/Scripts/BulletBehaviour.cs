using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Collider2D))]
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        if (collision.TryGetComponent(out EnemyStats enemyhealth))
        {
            enemyhealth.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
