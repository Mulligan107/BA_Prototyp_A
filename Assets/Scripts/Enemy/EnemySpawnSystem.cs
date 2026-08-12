using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private UpgradeSelectionController popup;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] float enemySpawnIntervall = 10f;
    
    private BoxCollider2D _boxCollider2D;
    private Transform _transform;
    
    private int _rounds;
    private int _enemyAmount = 10;
    private int _enemyHealthBonus;
    private float _enemySpeedBonus;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    private IEnumerator SpawnEnemyLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(enemySpawnIntervall);
        
        while (true)
        {
            yield return wait;
            
            _rounds += 1;
            
            if (_rounds % 3 == 0)
            {
                popup.OpenCardUpgradeGUI(stats);
                _enemyHealthBonus += 1;
                _enemyAmount += 5;
            }

            if (_rounds % 6 == 0)
            {
                _enemySpeedBonus += 1f;
            }
            
            Debug.Log("Rounds: " + _rounds);
            
            for (int i = 0; i < _enemyAmount; i++)
            {
                Vector2 spawnPoint = GetRandomPointonEdge();
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint, GetRotationTowardsMiddle(spawnPoint));
                enemy.GetComponent<EnemyStats>().AddBonusHealth(_enemyHealthBonus);
                enemy.GetComponent<EnemyController>().AddBonusSpeed(_enemySpeedBonus);
            }
            
            Debug.Log("Enemies Spawned: " + _enemyAmount);
        }
    }

    private Vector2 GetRandomPointonEdge()
    {
        Bounds b =  _boxCollider2D.bounds;
        
        float width = b.size.x;
        float height = b.size.y;
        
        //geht ne zufällige distanz um den Perimeter des GameObjects um zufällige Spawnpoints zu setzen
        float t = Random.Range(0f, 2f * (width + height));

        if (t < width) return new Vector2(b.min.x + t, b.min.y); // unten
        t -= width;
        
        if (t < width) return new Vector2(b.min.x + t, b.max.y); //oben
        t -= width;
        
        if (t < height) return new Vector2(b.min.x, b.min.y + t); //links 
        t -= height;
        
        return new Vector2(b.max.x, b.min.y + t); //rechts
    }

    private Quaternion GetRotationTowardsMiddle(Vector2 spawnPoint)
    {
        Vector2 dir = (Vector2)_boxCollider2D.bounds.center - spawnPoint;

        return Quaternion.LookRotation(Vector3.forward, dir);
    }
}
