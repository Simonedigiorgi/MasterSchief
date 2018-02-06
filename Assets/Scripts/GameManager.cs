using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator enemyAnimator;
    public PlayerActions player;
    public GameObject[] buttonPunch; //List of buttons
    int buttonChosen; //this button will be chosen random and activate to let our player punch

    public float secondsBeforeStart = 3;
    public float minDelay;
    public float maxDelay;
    [Range(0, 1)]
    public float buttonEventChance;


    HealthBar hb;

    private void Start()
    {
        hb = GameObject.Find("Background").GetComponent<HealthBar>();


        foreach (GameObject go in buttonPunch)
        {
            go.SetActive(false);
        }

        StartCoroutine(SecondsBeforeStart());
    }

    public IEnumerator SecondsBeforeStart()
    {
        yield return new WaitForSeconds(secondsBeforeStart);
        StartCoroutine(YouPunch());
    }

    public IEnumerator YouPunch()
    {
        buttonChosen = Random.Range(0, buttonPunch.Length);
        buttonPunch[buttonChosen].SetActive(true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        if (Random.value <= buttonEventChance)
            StartCoroutine(YouPunch());
        else StartCoroutine(YouParry());
    }

    public IEnumerator YouParry()
    {
        enemyAnimator.SetTrigger("chargePunch");

        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        if (Random.value <= buttonEventChance)
            StartCoroutine(YouPunch());
        else StartCoroutine(YouParry());
    }

    public void CheckIfParrying()
    {
        if(player.isParrying)
        {
            Debug.Log("PARATOH!!!!!!!!!!!!!");
        }
        else
        {
            hb.TakeDamage();
        }
    }

}
