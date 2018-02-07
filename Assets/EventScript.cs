using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    GameManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void CheckIfPlayerIsParrying()
    {
        StartCoroutine(manager.CheckIfParrying());
    }
}
