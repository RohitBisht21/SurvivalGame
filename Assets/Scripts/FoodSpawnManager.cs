using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class FoodType
    {
        public GameObject foodPrefab;
        public int maxActiveFoods = 10;
    }

    public List<FoodType> foodTypes = new List<FoodType>();
    public List<Transform> spawnPoints; // List of empty GameObjects where food will spawn

    private List<List<GameObject>> foodPools = new List<List<GameObject>>();

    private void Start()
    {
        InitializeFoodPools();
    }

    private void Update()
    {
        foreach (var foodType in foodTypes)
        {
            if (CountActiveFoods(foodType) < foodType.maxActiveFoods)
            {
                SpawnFood(foodType);
            }
        }
    }

    private void InitializeFoodPools()
    {
        foreach (var foodType in foodTypes)
        {
            List<GameObject> foodPool = new List<GameObject>();
            for (int i = 0; i < foodType.maxActiveFoods; i++)
            {
                GameObject food = Instantiate(foodType.foodPrefab, Vector3.zero, Quaternion.identity);
                food.SetActive(false);
                foodPool.Add(food);
            }
            foodPools.Add(foodPool);
        }
    }

    private void SpawnFood(FoodType foodType)
    {
        GameObject availableFood = GetInactiveFood(foodType);

        if (availableFood != null)
        {
            // Choose a random spawn point from the list
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Set the position of the food to the chosen spawn point and activate it
            availableFood.transform.position = randomSpawnPoint.position;
            availableFood.SetActive(true);
        }
    }

    private GameObject GetInactiveFood(FoodType foodType)
    {
        foreach (GameObject food in foodPools[foodTypes.IndexOf(foodType)])
        {
            if (!food.activeInHierarchy)
            {
                return food;
            }
        }
        return null;
    }

    private int CountActiveFoods(FoodType foodType)
    {
        int count = 0;
        foreach (GameObject food in foodPools[foodTypes.IndexOf(foodType)])
        {
            if (food.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}
