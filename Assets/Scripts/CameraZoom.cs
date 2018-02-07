using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    float startingSize;
    float startingAngle;

    public float sizeDecrement = 0.5f;
    public float tiltAngle = 20;
    public float zoomTime = 0.5f;
    public float zoomSpeed = 7;
    bool running;

    Camera cam;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();

        startingSize = cam.orthographicSize;
        startingAngle = transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            ZoomIn("left");

        if (Input.GetKeyDown(KeyCode.N))
            ZoomIn("right");
	}

    public void ZoomIn(string direction)
    {
        if (!running)
            StartCoroutine(ZoomTilt(direction));
    }

    IEnumerator ZoomTilt(string direction)
    {
        running = true;
        float timer = 0;
        Quaternion rot = Quaternion.identity;

        if (direction == "right")
            rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.x, tiltAngle);
        else
            rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.x, -tiltAngle);


        while (timer < zoomTime)
        {
            timer += Time.deltaTime;

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, startingSize - sizeDecrement, Time.deltaTime * zoomSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * zoomSpeed);

            yield return null;
        }

        timer = 0;

        rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.x, startingAngle);

        while (timer < zoomTime)
        {
            timer += Time.deltaTime;

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, startingSize, Time.deltaTime * zoomSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * zoomSpeed);

            yield return null;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, startingAngle);
        cam.orthographicSize = startingSize;

        running = false;
    }
}
