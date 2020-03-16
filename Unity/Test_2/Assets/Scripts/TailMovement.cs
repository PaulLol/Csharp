using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TailMovement : MonoBehaviour
{

    public float Speed;
    public Vector3 tailTarget;
    public MoveSnake mainSnake;
    public GameObject tailTargetObject;
    public int index;

    void Start()
    {
        mainSnake = GameObject.FindGameObjectWithTag("SnakeMain").GetComponent<MoveSnake>();
        tailTargetObject = mainSnake.tailObjects[mainSnake.tailObjects.Count - 2];
        index = mainSnake.tailObjects.IndexOf(gameObject);
    }
    void Update()
    {
        Speed = mainSnake.Speed;
        tailTarget = tailTargetObject.transform.position;
        transform.LookAt(tailTarget);
        transform.position = Vector3.Lerp(transform.position, tailTarget, Time.deltaTime * Speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SnakeMain"))
        {
            if(index >3)
            {
                SceneManager.LoadScene("Restart_scene");
            }
        }
    }
}
