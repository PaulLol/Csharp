using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGen : MonoBehaviour
{

    public float XSize = 23;
    public float ZSize = 23;
    public GameObject currentFood;
    public GameObject foodUp;
    public GameObject foodDown;
    public GameObject foodSpeed;
    public GameObject foodSlow;
    public GameObject foodReverse;
    public List<GameObject> foodPrefab = new List<GameObject>();
    public int foobPrefabCount;
    public Vector3 currentPosition;
    public int tailObjCount;
    public MoveSnake mainSnake;

    void Start()
    {
        mainSnake = GameObject.FindGameObjectWithTag("SnakeMain").GetComponent<MoveSnake>();
        foodPrefab.Add(foodUp);
        foodPrefab.Add(foodSpeed);
 
        RandomPosition();
        currentFood = GameObject.Instantiate(foodPrefab[0], currentPosition, Quaternion.identity);
    }

    void RandomPosition()
    {
        currentPosition = new Vector3(Random.Range(XSize * -1, XSize), 1.25f, Random.Range(ZSize * -1, ZSize));
    }
    public void AddNewFood()
    {
        RandomPosition();
        int rand = Random.Range(0,foodPrefab.Count);
        currentFood = GameObject.Instantiate(foodPrefab[rand], currentPosition, Quaternion.identity);
    }

    void Update()
    {
        if(tailObjCount > 0)
        {
            if (foodPrefab.Contains(foodDown) == false && foodPrefab.Contains(foodReverse) == false)
            {
                foodPrefab.Add(foodDown);
                foodPrefab.Add(foodReverse);
            }
            else { }
        }
        else
        {
            foodPrefab.Remove(foodDown);
            foodPrefab.Remove(foodReverse);
        }

        if (mainSnake.Speed > 7)
        {
            if (foodPrefab.Contains(foodSlow)==false)
            {
                foodPrefab.Add(foodSlow);
            }
            else{}
        }

        if (foodPrefab.Count == 4 && mainSnake.Speed < 7)
        {
            foodPrefab.Remove(foodSlow);
        }

        tailObjCount = mainSnake.tailObjects.Count - 1;

        if (!currentFood)
        {
            if (tailObjCount > 1)
            {
                AddNewFood();
            }
            else
            {
                RandomPosition();
                currentFood = GameObject.Instantiate(foodPrefab[0], currentPosition, Quaternion.identity);
            }
        }

        foobPrefabCount = foodPrefab.Count;
    }
}
