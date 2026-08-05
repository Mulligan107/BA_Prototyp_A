using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] float enemySpawnIntervall = 10f;
    
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemyLoop());
    }

    private IEnumerator spawnEnemyLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(enemySpawnIntervall);

        while (true)
        {
            yield return wait;

            // 10 gegner spawnen alle 10 sekunden
            for (int i = 0; i < 10; i++)
            {
                Instantiate(enemyPrefab, GetRandomPointonEdge(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPointonEdge()
    {
        Bounds b =  _boxCollider2D.bounds;
        
        float width = b.size.x;
        float height = b.size.y;
        
        //geht ne zufällige distanz um den Perimeter des GameObjects um zufällige Spawnpoints zu setzen
        float t = Random.Range(0f, 2f * (width + height));

        if (t < width) return new Vector3(b.min.x + t, b.min.y, 0f); // unten
        t -= width;
        
        if (t < width) return new Vector3(b.min.x + t, b.max.y, 0f); //oben
        t -= width;
        
        if (t < height) return new Vector3(b.min.x, b.min.y + t, 0f); //links 
        t -= height;
        
        return new Vector3(b.max.x, b.min.y + t, 0f); //rechts
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
