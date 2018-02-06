using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float maxBarLength = 10;
    public float barChangeAmount;
    Scrollbar bar;

	// Use this for initialization
	void Start () {
        bar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeValue(Type type)
    {
        if (type == Type.RED)
            bar.value -= barChangeAmount / maxBarLength;

        else if (type == Type.BLUE)
            bar.value += barChangeAmount / maxBarLength;
    }


}
