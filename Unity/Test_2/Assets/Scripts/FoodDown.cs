using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDown : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeMain"))
        {
            other.GetComponent<MoveSnake>().RemoveTail();
            other.GetComponent<MoveSnake>().DownSpeed(1);
            Destroy(gameObject);
        }
    }
}
