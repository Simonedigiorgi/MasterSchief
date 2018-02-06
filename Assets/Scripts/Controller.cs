using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private float speed = 200;


	void Start () {
		
	}
	
	void Update () {

        if (Input.GetMouseButton(1))
        {
            transform.GetChild(1).Rotate(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {
            transform.GetChild(2).Rotate(0, 0, -speed * Time.deltaTime);
        }

    }
}
