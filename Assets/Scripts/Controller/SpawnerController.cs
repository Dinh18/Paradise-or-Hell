using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController instant;
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
        EnemyHealth.OnEnemyKilled += SpawnEnemy;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupSpawnArea();
        InitializeEnemiesPool(50);
        UpdateWave(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        // currTime += Time.deltaTime;
        // if(currTime >= 20)
        // {
        //     Debug.Log("Space");
        //     numberOfTypeEnemies++;
        //     if(numberOfTypeEnemies > 3) numberOfTypeEnemies = 3;
        //     UpdateWave(numberOfTypeEnemies, 20);
        //     currTime = 0;
        // }
    }

    private void SetupSpawnArea()
    {
        Camera mainCamera = Camera.main;
        minRadius = mainCamera.orthographicSize;
        maxRadius = minRadius + 2;
    }

    private void InitializeEnemiesPool(int amount)
    {
        enemiesPoolDictionary = new Dictionary<string, List<GameObject>>();
        for(int i = 0; i < enemiesObjects.Count; i++)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for(int j = 0; j < amount; j++)
            {
                GameObject obj = Instantiate(enemiesObjects[i], transform); 
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            enemiesPoolDictionary.Add(enemiesObjects[i].name, objectPool);
        }
    }

    private void InitializeGemsPool(int amount)
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

    private void UpdateWave(int amountTypeEnemies, int amount)
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
        obj.SetActive(false);

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

    private bool IsValidPos(Vector2 pos)
    {
        return pos.x > 0 && pos.x < Constants.SIZE_MAP_X - 2 && pos.y > 0 && pos.y < Constants.SIZE_MAP_Y - 2;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(PlayerMovement.instant == null) return;
        Gizmos.DrawWireSphere(PlayerMovement.instant.playerPosition(), minRadius);
        Gizmos.DrawWireSphere(PlayerMovement.instant.playerPosition(), maxRadius);
    }

    
}
