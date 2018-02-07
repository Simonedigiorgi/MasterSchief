using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCountdown : MonoBehaviour {

    public Animator enemyAnimator;
    float timer = 0;
    public float countDownTime = 1;

    HealthBar hb;

    private void Awake()
    {
        hb = GameObject.Find("Background").GetComponent<HealthBar>();
    }

    // Use this for initialization
    void OnEnable () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer>countDownTime)
        {
            this.gameObject.SetActive(false);
            //enemyAnimator.SetTrigger("punch");
            //hb.TakeDamage();
        }
	}
}
