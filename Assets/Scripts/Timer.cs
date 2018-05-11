using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Image timerBar;
    private GameManager gameManager;
    private PlayerActions playerAction;
    private float timeLeft;

	void Start () {
        timerBar = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        playerAction = FindObjectOfType<PlayerActions>();
        timeLeft = gameManager.maxTime;
    }
	
	void Update () {

        if(FindObjectOfType<HealthBar>().isFinalPunches == true)
        {
            transform.parent.GetComponent<Image>().enabled = true;                                          // Abilita la barra di sfondo
            GetComponent<Image>().enabled = true;                                                           // Abilita la barra del Counter
            gameManager.counter.enabled = true;                                                             // Mostra il testo del Counter
            gameManager.pressButtonImage.enabled = true;
            gameManager.pressButtonImage.GetComponent<Animation>().Play("PressButton");

            if (gameManager.clickCounter < gameManager.finalPunches)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / gameManager.maxTime;
            }

            if (timerBar.fillAmount <= 0)
            {
                timeLeft = 0;
                playerAction.isLevelFailed = true;
                gameManager.pressButtonImage.GetComponent<Animation>().Stop("PressButton");
            }
        }
	}
}
