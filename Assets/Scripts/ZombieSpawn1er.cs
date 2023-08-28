using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn1er : MonoBehaviour
{
    [System.Serializable]
    public class ZombieType
    {
        public GameObject zombiePrefab;
        public int maxActiveZombies = 10;
    }

    public List<ZombieType> zombieTypes = new List<ZombieType>();
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    private List<List<GameObject>> zombiePools = new List<List<GameObject>>();

    private void Start()
    {
        InitializeZombiePools();
    }

    private void Update()
    {
        foreach (var zombieType in zombieTypes)
        {
            if (CountActiveZombies(zombieType) < zombieType.maxActiveZombies)
            {
                SpawnZombie(zombieType);
            }
        }
    }

    private void InitializeZombiePools()
    {
        foreach (var zombieType in zombieTypes)
        {
            List<GameObject> zombiePool = new List<GameObject>();
            for (int i = 0; i < zombieType.maxActiveZombies; i++)
            {
                GameObject zombie = Instantiate(zombieType.zombiePrefab, Vector3.zero, Quaternion.identity);
                zombie.SetActive(false);
                zombiePool.Add(zombie);
            }
            zombiePools.Add(zombiePool);
        }
    }

    private void SpawnZombie(ZombieType zombieType)
    {
        GameObject availableZombie = GetInactiveZombie(zombieType);

            if (availableZombie != null)
        {
        // Reset the zombie's properties
            availableZombie.GetComponent<EnemyController>().ResetZombieProperties();

        // Set the flag for zombies
            availableZombie.GetComponent<EnemyController>().isZombie = true;

        // Set the position and activate the zombie
            Vector3 randomSpawnPoint = GetRandomSpawnPoint();
            availableZombie.transform.position = randomSpawnPoint;
            availableZombie.SetActive(true);
        }
    }

    private GameObject GetInactiveZombie(ZombieType zombieType)
    {
        foreach (GameObject zombie in zombiePools[zombieTypes.IndexOf(zombieType)])
        {
            if (!zombie.activeInHierarchy)
            {
                return zombie;
            }
        }
        return null;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 min = spawnAreaCenter - spawnAreaSize / 2;
        Vector3 max = spawnAreaCenter + spawnAreaSize / 2;
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z)
        );
    }

    private int CountActiveZombies(ZombieType zombieType)
    {
        int count = 0;
        foreach (GameObject zombie in zombiePools[zombieTypes.IndexOf(zombieType)])
        {
            if (zombie.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe cube representing the spawn area size
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);

        // Draw a small sphere representing the spawn area center
        Gizmos.color = Color.blue; 
        Gizmos.DrawSphere(spawnAreaCenter, 0.2f); 
    }

}