using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BulletBehaviour : MonoBehaviour
{
    private float _lifeTime = 2f;
    private float _speed;
    private int _damage;

    public void Init(PlayerStats playerStats)
    {
        _speed = playerStats.BulletSpeed;
        _damage = playerStats.BulletDamage;
        transform.localScale = Vector3.one * playerStats.BulletSize;
        
        Destroy(gameObject, _lifeTime);
    }
    
    void Update()
    {
        transform.position += transform.right * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        if (collision.TryGetComponent(out EnemyStats enemyStats))
        {
            enemyStats.TakeDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}
