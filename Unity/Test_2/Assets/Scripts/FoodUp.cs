using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeMain"))
        {
            other.GetComponent<MoveSnake>().AddTail();
            other.GetComponent<MoveSnake>().AddSpeed(1);
            Destroy(gameObject);
        }
    }
}
