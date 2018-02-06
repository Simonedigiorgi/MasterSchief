using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    RED,
    BLUE,
    OBJECT
}

public class ObjectsCollisions : MonoBehaviour {


   

    public Type type;

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
            if(type == Type.RED)
            {
                Destroy(this.gameObject);
            }

        }

        if(other.gameObject.tag == "Blue")
        {
            if(type == Type.BLUE)
            {
                Destroy(this.gameObject);
            }
        }

        if(other.gameObject.tag == "Core")
        {
            if(type == Type.OBJECT)
            {
                //arrrivato al centro yay
            }
            else if(type == Type.RED)
            {
                manager.ChangeValue(type);
                Destroy(this.gameObject);
            }
            else if(type == Type.BLUE)
            {
                manager.ChangeValue(type);
                Destroy(this.gameObject);
            }
        }
    }
}
