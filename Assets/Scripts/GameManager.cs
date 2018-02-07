using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator enemyAnimator;
    public PlayerActions player;
    public GameObject[] buttonPunch; //List of buttons

    public float secondsBeforeStart = 3;
    public float delayBetweenButtons = 1;
    public float buttonActiveTime = 1;

    public float minButtonSpawnScale = 5;
    public float maxButtonSpawnScale = 7;

    [Range(0, 1)]
    public float buttonEventChance;
    [Range(1, 5)]
    public int maxButtonSpawnAmountPerEvent = 2;


    HealthBar hb;

    private void Start()
    {
        hb = GameObject.Find("Background").GetComponent<HealthBar>();


        foreach (GameObject go in buttonPunch)
        {
            go.GetComponent<ButtonCountdown>().countDownTime = buttonActiveTime;
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
        float nextCoroutine = 0;
        int value = Random.Range(1, maxButtonSpawnAmountPerEvent);

        for(int i = 0; i < value; i++)
        {
            GetRandomButton().SetActive(true);
            yield return new WaitForSeconds(delayBetweenButtons);
        }
        
        nextCoroutine += delayBetweenButtons > buttonActiveTime ? 0 : buttonActiveTime - delayBetweenButtons;

        yield return new WaitForSeconds(nextCoroutine);

        if (Random.value <= buttonEventChance)
        {
            StartCoroutine(YouPunch());
        }
        else
        {
            StartCoroutine(YouParry());
        }

    }

    public GameObject GetRandomButton()
    {
        int index = Random.Range(0, buttonPunch.Length);
        while(buttonPunch[index].activeSelf)
        {
            index = Random.Range(0, buttonPunch.Length);
        }

        float size = Random.Range(minButtonSpawnScale, maxButtonSpawnScale);
        buttonPunch[index].transform.localScale = new Vector3(size, size, 1);
        return buttonPunch[index];
    }

    public IEnumerator YouParry()
    {
        yield return null;
        enemyAnimator.SetTrigger("chargePunch");        
    }

    public IEnumerator CheckIfParrying()
    {
        bool trigger = false;
        bool trigger2 = false;

        while (!player.canParry)
        {
            if (player.isParrying && trigger2)
            {
                trigger2 = false;
                Debug.Log("PARATOH!!!!!!!!!!!!!");
            }
            else if(!trigger)
            {
                trigger = true;
                hb.TakeDamage();
            }
            yield return null;
        }
        

        if (Random.value <= buttonEventChance)
        {
            StartCoroutine(YouPunch());
        }
        else
        {
            StartCoroutine(YouParry());
        }
    }

}
