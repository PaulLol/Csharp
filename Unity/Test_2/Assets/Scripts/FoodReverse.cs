using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodReverse : MonoBehaviour
{
    public MoveSnake mainSnake;
    void OnTriggerEnter(Collider other)
    {
        mainSnake = GameObject.FindGameObjectWithTag("SnakeMain").GetComponent<MoveSnake>();

        Vector3 targetPosition = other.gameObject.transform.position;
        Vector3 newTargetPos = mainSnake.tailObjects[mainSnake.tailObjects.Count - 1].transform.position;
        other.gameObject.transform.position = newTargetPos;
        Quaternion targetRotation = other.gameObject.transform.rotation;
        targetRotation.y += 180;
        other.gameObject.transform.rotation = targetRotation;
        mainSnake.tailObjects[mainSnake.tailObjects.Count - 1].transform.position = targetPosition;
        Destroy(gameObject);
    }
}
