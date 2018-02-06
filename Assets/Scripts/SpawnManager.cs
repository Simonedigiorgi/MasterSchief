using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public Rigidbody2D[] rb;
    public float objectSpeed = 5;
    public float minSpeed = 50;
    public float maxSpeed = 70;

    public float secondsBeforeSpawning = 3;
    public float spawnRate = 1;
    public float spawnAmount;
    

    Vector3 botLeft;
    Vector3 botRight;
    Vector3 topLeft;
    Vector3 topRight;

    float spawnOffset = 5;

	// Use this for initialization
	void Start () {
        

        botLeft.z = 0;
        botRight.z = 0;
        topLeft.z = 0;
        topRight.z = 0;

        StartCoroutine(SecondsBeforeSpawn());
    }

    IEnumerator SecondsBeforeSpawn()
    {
        yield return new WaitForSeconds(secondsBeforeSpawning);
        StartCoroutine(SpawnOverTime());
    }

    IEnumerator SpawnOverTime()
    {
        for(int i=0; i<spawnAmount; i++)
        {
            SpawnObject();
        }
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnOverTime());
    }

    // Update is called once per frame
    void Update () {
        botLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        botRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    public void SpawnObject()
    {
        Rigidbody2D clone = Instantiate(rb[Random.Range(0, rb.Length)], GetRandomPosition(), Quaternion.identity);
        Vector3 dir = Vector3.zero - clone.transform.position;
        clone.transform.up = dir;
        clone.AddRelativeForce(new Vector3(0,1) * Random.Range(minSpeed,maxSpeed));       
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
