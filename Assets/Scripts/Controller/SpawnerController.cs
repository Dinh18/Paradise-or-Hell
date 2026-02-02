using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController instant;
    [Header("Pool Setting")]
    [SerializeField] private int objPerFrame = 5; 
    // Pooling Enemies
    [SerializeField] private List<GameObject> enemiesObjects;
    private Dictionary<string, List<GameObject>> enemiesPoolDictionary;
    // Pooling Gems
    [SerializeField] private List<GameObject> gemsObjects;
    private Dictionary<string, List<GameObject>> gemsPoolDictionary;
    private float minRadius;
    private float maxRadius;
    void Awake()
    {
        instant = this;
    }
    void OnEnable()
    {
        // EnemyHealth.OnEnemyKilled += SpawnGem;
        EnemyHealth.OnEnemyKilled += SpawnEnemy;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        SetupSpawnArea();
        // InitializeEnemiesPool(50);
        // InitializeGemsPool(50);
        yield return StartCoroutine(InitializeAllPool());
        
        UpdateWave(1, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator InitializeAllPool()
    {
        enemiesPoolDictionary = new Dictionary<string, List<GameObject>>();
        gemsPoolDictionary = new Dictionary<string, List<GameObject>>();

        yield return StartCoroutine(InitializeEnemiesPool(50));
        yield return StartCoroutine(InitializeGemsPool(50));
    }

    private void SetupSpawnArea()
    {
        Camera mainCamera = Camera.main;
        minRadius = mainCamera.orthographicSize;
        maxRadius = minRadius + 2;
    }

    private IEnumerator InitializeEnemiesPool(int amount)
    {
        for(int i = 0; i < enemiesObjects.Count; i++)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for(int j = 0; j < amount; j++)
            {
                GameObject obj = Instantiate(enemiesObjects[i], transform); 
                obj.SetActive(false);
                objectPool.Add(obj);

                if(j % objPerFrame == 0)
                {
                    yield return null;
                }
            }
            enemiesPoolDictionary.Add(enemiesObjects[i].name, objectPool);
        }
    }

    private IEnumerator InitializeGemsPool(int amount)
    {
        gemsPoolDictionary = new Dictionary<string, List<GameObject>>();
        for(int i = 0; i < gemsObjects.Count; i++)
        {
            List<GameObject> objectsPool = new List<GameObject>();
            for(int j = 0; j < amount; j++)
            {
                GameObject obj = GameObject.Instantiate(gemsObjects[i], transform);
                obj.SetActive(false);
                objectsPool.Add(obj);

                if(j % objPerFrame == 0)
                {
                    yield return null;
                }
            }
            gemsPoolDictionary.Add(gemsObjects[i].name,objectsPool);
        }
    }

    private Vector2 GetSpawnPosition()
    {
        // 1. Lấy một hướng ngẫu nhiên (Vector độ dài = 1)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // 2. Random khoảng cách từ min đến max
        float randomDistance = Random.Range(minRadius, maxRadius);

        // 3. Tính vị trí cuối cùng
        // PlayerPos + (Hướng * Khoảng cách)
        Vector2 spawnPos = (Vector2)PlayerController.instant.PlayerPosition() + randomDirection * randomDistance;

        return spawnPos;
    }

    public void UpdateWave(int amountTypeEnemies, int amount)
    {
        for(int i = 0; i < amountTypeEnemies; i++)
        {
            for(int j = 0; j < amount; j++)
            {
                GameObject obj = enemiesPoolDictionary[enemiesObjects[i].name][j];
                if(!obj.activeSelf) SpawnEnemy(obj);
            }
        }
    }

    public void SpawnEnemy(GameObject obj)
    {
        if(obj.activeSelf)
        {
            SpawnGem(obj);
            obj.SetActive(false);
        }
        
        Vector2 spawnPos = GetSpawnPosition();

        while(!IsValidPos(spawnPos))
        {
            spawnPos = GetSpawnPosition();
        }

        EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
        if(enemyHealth != null) enemyHealth.ReSpawn();
        obj.transform.position = spawnPos;
        obj.SetActive(true);
    }

    private void SpawnGem(GameObject obj)
    {
        string nameObj = obj.GetComponent<EnemyHealth>().GetExpGemPrefab().name;
        foreach(var objGem in gemsPoolDictionary[nameObj])
        {
            if(!objGem.activeSelf)
            {
                objGem.transform.position = obj.transform.position;
                objGem.SetActive(true);
                break;
            }
            
        }
    }

    private bool IsValidPos(Vector2 pos)
    {
        return pos.x > 2 && pos.x < Constants.SIZE_MAP_X - 2 && pos.y > 2 && pos.y < Constants.SIZE_MAP_Y - 2;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(PlayerMovement.instant == null) return;
        Gizmos.DrawWireSphere(PlayerMovement.instant.playerPosition(), minRadius);
        Gizmos.DrawWireSphere(PlayerMovement.instant.playerPosition(), maxRadius);
    }

    
}
