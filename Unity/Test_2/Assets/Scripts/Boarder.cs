using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boarder : MonoBehaviour
{
    public float XPos;
    public float ZPos;
    void OnTriggerEnter(Collider other)
    {

        Vector3 targetPosition = other.gameObject.transform.position;
        targetPosition.x *= XPos;
        targetPosition.z *= ZPos;
        other.gameObject.transform.position = targetPosition;
    }

}
