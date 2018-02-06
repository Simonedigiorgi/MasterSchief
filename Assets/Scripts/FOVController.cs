using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour {

    public Camera maincamera;
    public float FOV, speed;

	void Update () {
        maincamera.fieldOfView = Mathf.MoveTowards(maincamera.fieldOfView, FOV, Time.deltaTime * speed);
    }

}
