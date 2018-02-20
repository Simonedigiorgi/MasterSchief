using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCountdown : MonoBehaviour {

    private GameManager gameManager;                                                      // GAMEMANGAER
    private HealthBar healthBar;                                                          // HEALTHBAR
    private PlayerActions playerAction;

    private float timer = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        healthBar = FindObjectOfType<HealthBar>();
        playerAction = FindObjectOfType<PlayerActions>();
    }

    void OnEnable () {

        timer = 0;
	}
	
	void Update () {

        timer += Time.deltaTime;

        // Alla sparizione del Tasto

        if((timer > gameManager.buttonTime) && playerAction.isActive == true)
        {
            gameObject.SetActive(false);
            gameManager.chefAnimator.Play("CounterAttack");
            healthBar.TakeDamage();
        }
	}
}
