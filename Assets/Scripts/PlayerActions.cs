using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {

    public LayerMask buttonMask;
    public LayerMask enemyMask;

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

    public Image blackFade;




    public float endGameTime = 30;
    float endTimer = 0;
    public int endGameClickAmount = 40;
    int clickCounter = 0;

    bool levelFinished = false;
    bool levelFailed = false;

    public string nextScene = "Cannavacciuolo";


    public GameObject[] enemiesPunch;
    public GameObject[] playerPunch;
    public GameObject parat;

    public Transform destra;
    public Transform sinistra;
    public Transform p;

	// Use this for initialization
	void Start () {
        hb = GameObject.Find("Background").GetComponent<HealthBar>();
        canParryTimer = parryCooldown;
    }


    bool toggle = false;


    public void SpawnPunchInfame()
    {
        Instantiate(enemiesPunch[0], p);
    }

    public void SpawnChargeInfame()
    {
        Instantiate(enemiesPunch[1], p);
    }

    public void SpawnPlayerInfame()
    {
        if(Random.value > 0.5f)
        {
            GameObject s = Instantiate(playerPunch[Random.Range(0, playerPunch.Length)], destra);
            s.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
        else
        {
            GameObject s = Instantiate(playerPunch[Random.Range(0, playerPunch.Length)], sinistra);
            s.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }

    public void SpawnParat()
    {
        GameObject s = Instantiate(parat, p);
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            if (hb.endGame && !levelFinished && !levelFailed)
            {
                if (hb.hasWon)
                {
                    endTimer += Time.deltaTime;
                    if (endTimer > endGameTime)
                        hb.hasWon = false;

                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, enemyMask);

                    if (hit.collider != null)
                    {
                        //animazione hit boss

                        if (toggle)
                        {
                            toggle = !toggle;
                            leftArmAnimator.SetTrigger("punch");
                            SpawnPlayerInfame();
                        }
                        else
                        {
                            toggle = !toggle;
                            rightArmAnimator.SetTrigger("punch");
                            SpawnPlayerInfame();
                        }

                        enemyAnimator.SetTrigger("takeDamage");

                        clickCounter++;
                        if (clickCounter >= endGameClickAmount)
                        {
                            levelFinished = true;
                        }

                    }
                }
                else
                {
                    levelFailed = true;
                }
            }
            else if (!isParrying && !hb.endGame)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, buttonMask);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "ButtonLeft")
                    {
                        leftArmAnimator.SetTrigger("punch");
                        hit.collider.gameObject.SetActive(false);
                        hb.EnemyDamage("left");
                        SpawnPlayerInfame();

                    }
                    else if (hit.collider.tag == "ButtonRight")
                    {
                        rightArmAnimator.SetTrigger("punch");
                        hit.collider.gameObject.SetActive(false);
                        hb.EnemyDamage("right");
                        SpawnPlayerInfame();
                    }
                    else
                    {
                        enemyAnimator.SetTrigger("punch");
                    }
                }
                else
                {
                    enemyAnimator.SetTrigger("punch");
                }
            }
        }
        else if(Input.GetMouseButtonDown(1) && !hb.endGame)
        {
            if(canParry)
            {
                isParrying = true;
                canParry = false;
                canParryTimer = 0;
            }
        }
        
        if(isParrying)
        {
            parryTimer += Time.deltaTime;
            if(parryTimer>=parryTime)
            {
                isParrying = false;
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


        if (levelFinished)
        {
            blackFade.gameObject.SetActive(true);
            fadeTimer += Time.deltaTime;

            blackFade.color = new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, fadeTimer / fadeTime);

            if(fadeTimer>fadeTime)
            {
                SceneManager.LoadScene(nextScene);
            }
            
        }

        if (levelFailed)
        {
            Debug.Log("d");
            blackFade.gameObject.SetActive(true);
            fadeTimer += Time.deltaTime;

            blackFade.color = new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, fadeTimer / fadeTime);

            this.transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);

            if (!endAnimTrigger)
            {
                enemyAnimator.SetTrigger("charge");
                endAnimTrigger = true;
            }

            if (fadeTimer > fadeTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }


	}

    bool endAnimTrigger = false;

    float fadeTime = 4;
    float fadeTimer = 0;


}
