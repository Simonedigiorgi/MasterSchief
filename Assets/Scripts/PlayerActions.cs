using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public LayerMask buttonMask;

    public Animator leftArmAnimator;
    public Animator rightArmAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Camera.main.transform.forward, 100, buttonMask);
            if (hit)
            {
                if (hit.collider.tag == "ButtonLeft")
                {
                    leftArmAnimator.SetTrigger("punch");
                }
                else if (hit.collider.tag == "ButtonRight")
                {
                    rightArmAnimator.SetTrigger("punch");
                }
                else
                {

                }
            }
            else
            {

            }
        }
	}
}
