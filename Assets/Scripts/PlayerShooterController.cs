using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float fireRate = 0.5f;
    
    private GameObject[] _enemyList;
    private float _nextShotTime;
    private float _refreshTimer;
    private const float RefreshInterval = 0.2f; //für Deltatime

    // Update is called once per frame
    private void Update()
    {
        _refreshTimer -= Time.deltaTime;
        if (_refreshTimer <= 0f)
        {
            _enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            _refreshTimer = RefreshInterval;
        }

        if (Time.time >= _nextShotTime)
        {
            if (Shoot())
            {
                _nextShotTime = Time.time + fireRate;
            }
        }
    }

    private GameObject LookForNearestEnemy()
    {
        if (_enemyList == null) return null;

        GameObject targetEnemy = null;
        float targetDistanceSqrd = maxDistance * maxDistance;

        foreach (var enemy in _enemyList)
        {
            if (enemy == null) continue; // kann zerstört worden sein

            Vector2 relativePosition = enemy.transform.position - transform.position;
            float distanceSqrd = relativePosition.sqrMagnitude;

            if (distanceSqrd < targetDistanceSqrd)
            {
                targetDistanceSqrd = distanceSqrd;
                targetEnemy = enemy;
            }
        }

        return targetEnemy;
    }
    
    private bool Shoot()
    {
        GameObject target = LookForNearestEnemy();
        if (target == null) return false;

        Vector2 targetDirection = target.transform.position - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection.normalized;

        return true;
    }
}
