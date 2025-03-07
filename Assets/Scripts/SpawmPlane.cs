using System.Collections;
using UnityEngine;

public class SpawnPlane : MonoBehaviour
{
    public GameObject planePrefab;
    public GameObject[] spawnPoints;
    public float spawnRate = 0.5f;
    public float flySpeed = -3f;
    public int maxPlanes = 5;
    public float spawnIntervalVariance = 0.5f;
    public float destroyDelay = 5f;  // Thêm biến để tùy chỉnh thời gian hủy

    private int currentPlanes = 0;
    private int currentSpawnPointIndex = 0;

    private void Start()
    {
        if (planePrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Thiếu prefab hoặc spawn points!");
            return;
        }

        StartCoroutine(SpawnPlaneRoutine());
    }

    IEnumerator SpawnPlaneRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / spawnRate);

            if (currentPlanes < maxPlanes)
            {
                SpawnPlaneObject();
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnPlaneObject()
    {
        GameObject spawnPoint = spawnPoints[currentSpawnPointIndex];
        currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;

        GameObject plane = Instantiate(planePrefab, spawnPoint.transform.position, Quaternion.identity);

        Rigidbody2D rb = plane.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = new Vector2(flySpeed, 0);

        currentPlanes++;
        plane.AddComponent<PlaneLifecycle>().Init(this);

        Destroy(plane, destroyDelay);  // Sử dụng destroyDelay thay vì giá trị cứng 5f
    }

    public void DecreasePlaneCount()
    {
        currentPlanes--;
    }
}

public class PlaneLifecycle : MonoBehaviour
{
    private SpawnPlane spawner;

    public void Init(SpawnPlane spawner)
    {
        this.spawner = spawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.DecreasePlaneCount();
    }
}