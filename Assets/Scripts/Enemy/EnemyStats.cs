using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 2;
    
    private int _pointRewardOnKill = 10;
    private int _currentHealth;
    private bool _isDead;
    
    public static event Action<int> Died;

    void Awake()
    {
        _currentHealth = enemyHealth;
    }

    public void AddBonusHealth(int amount)
    {
        enemyHealth += amount;
        _currentHealth = enemyHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if (_isDead)  return;
        
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke(_pointRewardOnKill);
        Destroy(gameObject);
    }
}
