using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSlow : MonoBehaviour
{
    public Collider col;
    void OnTriggerEnter(Collider other)
    {
        col = other;
        if (other.CompareTag("SnakeMain"))
        {
            other.GetComponent<MoveSnake>().DownSpeed(5);
            StartCoroutine(wait(5));
        }
    }

    IEnumerator wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        col.GetComponent<MoveSnake>().AddSpeed(5);
        Destroy(gameObject);
    }
}
