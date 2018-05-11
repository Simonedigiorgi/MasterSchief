using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Image timerBar;
    public Image timerPanel;

    public Text impiattaloText;                                                                         // Testo Impiattalo!!

    private PlayerActions playerAction;
    private FinalPunches finalPunches;                                                                      // FINALPUNCHES
    private HealthBar healthBar;

    private float timeLeft;

    void Start () {

        playerAction = FindObjectOfType<PlayerActions>();

        healthBar = GetComponent<HealthBar>();
        finalPunches = GetComponent<FinalPunches>();

        timeLeft = finalPunches.maxTime;

        timerPanel.enabled = false;                                                                      // Nascondi la barra di sfondo
        timerBar.enabled = false;                                                                        // Nascondi la barra del Counter
        finalPunches.counterText.enabled = false;                                                        // Nascondi il testo del Counter
        finalPunches.pressButtonImage.enabled = false;                                                   // Nascondi il Tasto
        impiattaloText.enabled = false;
    }
	
	void Update () {

        if(healthBar.isFinalPunches == true)
        {
            impiattaloText.enabled = true;
            impiattaloText.GetComponent<Animation>().Play("MoveFromLeft");

            if (playerAction.canFinalPunches == true)
            {
                impiattaloText.enabled = false;

                timerPanel.enabled = true;                                                                   // Mostra la barra di sfondo
                timerBar.enabled = true;                                                                     // Mostra la barra del Counter
                finalPunches.counterText.enabled = true;                                                     // Mostra il testo del Counter
                finalPunches.pressButtonImage.enabled = true;                                                // Mostra il Tasto
            }

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
