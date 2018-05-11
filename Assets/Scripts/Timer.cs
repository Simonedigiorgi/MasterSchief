using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Image timerBar;
    public Image timerPanel;

    private PlayerActions playerAction;
    private FinalPunches finalPunches;                                                                      // FINALPUNCHES
    private HealthBar healthBar;

    private float timeLeft;

	void Start () {

        playerAction = FindObjectOfType<PlayerActions>();

        healthBar = GetComponent<HealthBar>();
        finalPunches = GetComponent<FinalPunches>();

        timeLeft = finalPunches.maxTime;
    }
	
	void Update () {

        if(healthBar.isFinalPunches == true)
        {
            timerPanel.enabled = true;                                          // Abilita la barra di sfondo
            timerBar.enabled = true;                                                           // Abilita la barra del Counter

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
