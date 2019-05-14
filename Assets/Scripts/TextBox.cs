using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour {

    public string inputString = "";
    int lengthOfField = 30;

    // Use this for initialization
    void Start () {
        //OnGUI();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetString("input text", inputString);
    }

    private void OnGUI()
    {
        inputString = GUILayout.TextField(inputString, lengthOfField);
    }
}
