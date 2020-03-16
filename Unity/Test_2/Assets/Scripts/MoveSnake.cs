using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnake : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public List<GameObject> tailObjects = new List<GameObject>();
    public float distance = 0.8f;
    public GameObject tailPrefab;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        tailObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up *-1* RotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            canvas.SetActive(true);

        }
    }



    public void AddTail()
    {
        Vector3 newTailPos = tailObjects[tailObjects.Count-1].transform.position;
        newTailPos.z -= distance;
        tailObjects.Add(GameObject.Instantiate(tailPrefab, newTailPos, Quaternion.identity));
    }
    public void RemoveTail()
    {
        Destroy(tailObjects[tailObjects.Count - 1]);
        tailObjects.RemoveAt(tailObjects.Count-1);
    }

    public void AddSpeed(int speedUp)
    {
        Speed += speedUp;
    }

    public  void DownSpeed(int speedDown)
    {
        Speed -= speedDown;
    }


}
