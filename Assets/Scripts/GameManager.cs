using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] buttonPunch; //List of buttons
    int buttonChosen; //this button will be chosen random and activate to let our player punch
    public int countDown;

    public float secondsBeforeStart = 3;
    public float minDelay;
    public float maxDelay;
    [Range(0, 1)]
    public float buttonEventChance;
    public IEnumerator SecondsBeforeStart()
    {
        yield return new WaitForSeconds(secondsBeforeStart);
        YouPunch();
    }

    public IEnumerator YouPunch()
    {
        countDown = 3;
        buttonChosen = Random.Range(0, buttonPunch.Length);
        buttonPunch[buttonChosen].SetActive(true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        if (Random.value <= buttonEventChance)
            StartCoroutine(YouPunch());
        else StartCoroutine(YouParry());
    }

    public IEnumerator YouParry()
    {
        return null;
    }

}
