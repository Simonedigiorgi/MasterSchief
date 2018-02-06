using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Rigidbody2D[] rb;
    public float speed = 5;

    Vector3 botLeft;
    Vector3 botRight;
    Vector3 topLeft;
    Vector3 topRight;

    float spawnOffset = 5;

	// Use this for initialization
	void Start () {
        botLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        botRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        botLeft.z = 0;
        botRight.z = 0;
        topLeft.z = 0;
        topRight.z = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
            SpawnObject();
	}

    public void SpawnObject()
    {
        Rigidbody2D clone = Instantiate(rb[Random.Range(0, rb.Length)], GetRandomPosition(), Quaternion.identity);
        Vector3 dir = Vector3.zero - clone.transform.position;
        clone.transform.up = dir;
        clone.AddRelativeForce(new Vector3(0,1) * speed);       
    }

    public Vector3 GetRandomPosition()
    {
        float x = 0;
        float y = 0;

        while(x >= botLeft.x && x <= botRight.x && y >= botLeft.y && y <= topRight.y)
        {
            x = Random.Range(botLeft.x- spawnOffset, botRight.x + spawnOffset);
            y = Random.Range(botLeft.y-spawnOffset, topLeft.y+spawnOffset);
        }

        return new Vector3(x, y, 0);
    }

}
