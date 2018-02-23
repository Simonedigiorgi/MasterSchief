using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

    private GameManager gameManager;                                                                // GAMEMANAGER
    private HealthBar healthBar;                                                                    // HEALTHBAR
    private PlayerActions playerAction;                                                             // PLAYERACTION

	void Start () {

        gameManager = FindObjectOfType<GameManager>();                                              // GAMEMANAGER
        healthBar = FindObjectOfType<HealthBar>();                                                  // HEALTHBAR
        playerAction = FindObjectOfType<PlayerActions>();                                           // PLAYERACTION
    }
	
	void CheckIfPlayerIsParrying()
    {
        if(healthBar.playerLife != 0)
        {
            gameManager.currentCoroutine = gameManager.CheckIfParrying();
            StartCoroutine(gameManager.currentCoroutine);
        } 
    }

    // DANNEGGIA IL PLAYER

    void DamagePlayer()
    {
        if(playerAction.isActive == true)
        {
            healthBar.TakeDamage();
            playerAction.SpawnPunchInfame();
        }
    }
}
