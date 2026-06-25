using System.Collections.Generic;
using UnityEngine;

public class TrafficPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] aiCarPrefabs;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private float trafficSpeed = 5f;
    [SerializeField] private PathWaypoints trafficPath;

    private Queue<GameObject> carPool = new Queue<GameObject>();
    private List<GameObject> masterCarList = new List<GameObject>();
    private float spawnTimer;
    private float currentSpawnInterval;
    private bool isPoolActive = false;

    public float TrafficSpeed => trafficSpeed;

    private void OnEnable()
    {
        UIManager.OnNextLevelLoad += HandleLevelTransitionCleanup;
    }

    private void OnDisable()
    {
        UIManager.OnNextLevelLoad -= HandleLevelTransitionCleanup;
    }

    private void Start()
    {
        if (aiCarPrefabs == null || aiCarPrefabs.Length == 0)
        {
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            int prefabIndex = i % aiCarPrefabs.Length;
            GameObject car = Instantiate(aiCarPrefabs[prefabIndex]);
            car.SetActive(false);

            AICarMovementController trafficScript = car.GetComponent<AICarMovementController>();
            if (trafficScript != null)
            {
                trafficScript.SetPoolManager(this);
            }

            carPool.Enqueue(car);
            masterCarList.Add(car);
        }

        SpawnVehicleFromPool();

        spawnTimer = 0f;
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        isPoolActive = true;
    }

    private void Update()
    {
        if (!isPoolActive) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnVehicleFromPool();
            spawnTimer = 0f;
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void SpawnVehicleFromPool()
    {
        if (carPool.Count > 0)
        {
            GameObject car = carPool.Dequeue();

            AICarMovementController trafficScript = car.GetComponent<AICarMovementController>();
            if (trafficScript != null)
            {
                trafficScript.ResetToStartPath(trafficPath);
            }

            car.SetActive(true);
        }
    }

    public void ReturnCarToPool(GameObject car)
    {
        if (car == null) return;

        car.SetActive(false);
        carPool.Enqueue(car);
    }

    private void HandleLevelTransitionCleanup()
    {
        ClearAndDestroyPool();
    }

    public void ClearAndDestroyPool()
    {
        isPoolActive = false;
        carPool.Clear();

        for (int i = 0; i < masterCarList.Count; i++)
        {
            if (masterCarList[i] != null)
            {
                Destroy(masterCarList[i]);
            }
        }

        masterCarList.Clear();
    }
}