using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    GameManager manager;
    HealthBar hb;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hb = GameObject.FindObjectOfType<HealthBar>();
	}
	
	void CheckIfPlayerIsParrying()
    {
        if(!hb.endGame)
        {
            manager.currentCoroutine = manager.CheckIfParrying();
            StartCoroutine(manager.currentCoroutine);
        }
       
    }

    void DamagePlayer()
    {
        hb.TakeDamage();
    }
}
