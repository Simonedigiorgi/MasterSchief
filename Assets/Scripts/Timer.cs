using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Image timerBar;
    private GameManager gameManager;
    private PlayerActions playerAction;
    public float maxTime = 5f;
    private float timeLeft;

	void Start () {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        gameManager = FindObjectOfType<GameManager>();
        playerAction = FindObjectOfType<PlayerActions>();
	}
	
	void Update () {

        if(FindObjectOfType<HealthBar>().isFinalPunches == true)
        {
            transform.parent.GetComponent<Image>().enabled = true;
            GetComponent<Image>().enabled = true;

            if (gameManager.clickCounter < gameManager.finalPunches)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / maxTime;
            }

            if (timerBar.fillAmount <= 0)
            {
                timeLeft = 0;
                playerAction.isLevelFailed = true;
            }
        }


	}
}
