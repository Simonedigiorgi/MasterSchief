using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfameFade : MonoBehaviour {

    float timer = 0;
    float time = 1;

    SpriteRenderer s;


	// Use this for initialization
	void Start () {
        s = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        s.color = new Color(s.color.r, s.color.g, s.color.b, 1 - timer / time);
        if (timer > time)
            Destroy(this.gameObject);
	}
}
