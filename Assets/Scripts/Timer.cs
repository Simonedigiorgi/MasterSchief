using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Image timerBar;
    private PlayerActions playerAction;
    private FinalPunches finalPunches;                                                                      // FINALPUNCHES
    private float timeLeft;

	void Start () {
        timerBar = GetComponent<Image>();
        playerAction = FindObjectOfType<PlayerActions>();
        finalPunches = FindObjectOfType<FinalPunches>();
        timeLeft = finalPunches.maxTime;
    }
	
	void Update () {

        if(FindObjectOfType<HealthBar>().isFinalPunches == true)
        {
            transform.parent.GetComponent<Image>().enabled = true;                                          // Abilita la barra di sfondo
            GetComponent<Image>().enabled = true;                                                           // Abilita la barra del Counter
            finalPunches.counterText.enabled = true;                                                             // Mostra il testo del Counter
            finalPunches.pressButtonImage.enabled = true;
            finalPunches.pressButtonImage.GetComponent<Animation>().Play("PressButton");

            if (finalPunches.clickCounter < finalPunches.punches)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / finalPunches.maxTime;
            }

            if (timerBar.fillAmount <= 0)
            {
                timeLeft = 0;
                playerAction.isLevelFailed = true;
                finalPunches.pressButtonImage.GetComponent<Animation>().Stop("PressButton");
            }
        }
	}
}
