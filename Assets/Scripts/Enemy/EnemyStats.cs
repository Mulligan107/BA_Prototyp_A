using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 2;
    
    private int _currentHealth;

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
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
