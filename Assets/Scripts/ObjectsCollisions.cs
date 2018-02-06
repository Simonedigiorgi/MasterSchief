using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnColor
{
    RED,
    BLUE,
    OBJECT
}

public class ObjectsCollisions : MonoBehaviour {

   


    public SpawnColor type;

    GameManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Red")
        {
            if(type == SpawnColor.RED)
            {
                Destroy(this.gameObject);
            }

        }

        if(other.gameObject.tag == "Blue")
        {
            if(type == SpawnColor.BLUE)
            {
                Destroy(this.gameObject);
            }
        }

        if(other.gameObject.tag == "Core")
        {
            if(type == SpawnColor.OBJECT)
            {
                //arrrivato al centro yay
            }
            else if(type == SpawnColor.RED)
            {
                manager.ChangeValue(type);
                Destroy(this.gameObject);
            }
            else if(type == SpawnColor.BLUE)
            {
                manager.ChangeValue(type);
                Destroy(this.gameObject);
            }
        }
    }
}
