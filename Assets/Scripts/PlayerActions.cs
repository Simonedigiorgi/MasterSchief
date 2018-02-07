using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public LayerMask buttonMask;

    public Animator leftArmAnimator;
    public Animator rightArmAnimator;
    public Animator enemyAnimator;

    public bool isParrying = false;
    public float parryTime = 0.5f;
    public float parryCooldown = 2;
    float parryTimer = 0;

    [HideInInspector]
    public bool canParry = true;
    [HideInInspector]
    public float canParryTimer = 0;

    HealthBar hb;

	// Use this for initialization
	void Start () {
        hb = GameObject.Find("Background").GetComponent<HealthBar>();
        canParryTimer = parryCooldown;
    }





    // Update is called once per frame
    void Update () {

        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, Color.red);

		if (Input.GetMouseButtonDown(0))
        {
            if (hb.endGame)
            {
                if (hb.hasWon)
                {

                }
                else
                {

                }
            }
            else
            if (!isParrying)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, buttonMask);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "ButtonLeft")
                    {
                        leftArmAnimator.SetTrigger("punch");
                        hit.collider.gameObject.SetActive(false);
                        hb.EnemyDamage();

                    }
                    else if (hit.collider.tag == "ButtonRight")
                    {
                        rightArmAnimator.SetTrigger("punch");
                        hit.collider.gameObject.SetActive(false);
                        hb.EnemyDamage();
                    }
                    else
                    {
                        enemyAnimator.SetTrigger("punch");
                        hb.TakeDamage();
                    }
                }
                else
                {
                    enemyAnimator.SetTrigger("punch");
                    hb.TakeDamage();
                }
            }
        }
        else if(Input.GetMouseButtonDown(1) && !hb.endGame)
        {
            if(canParry)
            {
                isParrying = true;
                canParry = false;
            }
        }
        
        if(isParrying)
        {
            parryTimer += Time.deltaTime;
            if(parryTimer>=parryTime)
            {
                isParrying = false;
                canParryTimer = 0;
                parryTimer = 0;
            }
        }

        leftArmAnimator.SetBool("parry", isParrying);
        rightArmAnimator.SetBool("parry", isParrying);


        canParryTimer += Time.deltaTime;

        if(canParryTimer>parryCooldown)
        {
            canParry = true;
        }
        else
        {
            canParry = false;
        }
	}



}
