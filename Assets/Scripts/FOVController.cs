using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour {

    public Camera maincamera;
    public float FOV, speed;

	void Update () {
        maincamera.orthographicSize = Mathf.MoveTowards(maincamera.orthographicSize, FOV, Time.deltaTime * speed);
    }

}
