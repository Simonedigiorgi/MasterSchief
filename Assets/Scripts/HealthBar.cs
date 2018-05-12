using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class HealthBar : MonoBehaviour {

    private CameraZoom cameraZoom;                                                  // CAMERAZOOM
    private GameManager gameManager;                                                // GAMEMANAGER
    private PlayerActions playerAction;                                             // PLAYERACTION
    private CameraShake cameraShake;                                                // CAMERASHAKE
    
    [FoldoutGroup("Immagini")] public Image playerHealth;                           // Barra dell'energia del Player
    [FoldoutGroup("Immagini")] public Image chefHealth;                             // Barra dell'energia dello Chef
    [FoldoutGroup("Immagini")] public Image chefPanel;                              // Barra dietro l'energia dello Chef

    [FoldoutGroup("Testi")] public Text playerText;                                 // Testo Player
    [FoldoutGroup("Testi")] public Text chefText;                                   // Testo Chef
    [FoldoutGroup("Testi")] public string chefName;                                 // Nome dello Chef

    [BoxGroup("Barre della Vita")] [Range(0, 100)] public float playerLife = 60;    // Energia del Player
    [BoxGroup("Barre della Vita")] [Range(0, 100)] public float chefLife = 60;      // Energia dello Chef

    private float playerMaxLife = 100;                                              // Energia Massima del Player
    private float chefMaxLife = 100;                                                // Energia Massima dello Chef

    [BoxGroup("Danno dei Colpi")] public float playerDamage = 0;                    // Danno del Player
    [BoxGroup("Danno dei Colpi")] public float chefDamage = 0;                      // Danno dello Chef

    [HideInInspector] public bool isFinalPunches = false;                           // Fase finale

	void Start () {

        PlayerHealthBar();                                                          // PLAYER LIFEBAR
        ChefHealthBar();                                                            // CHEF LIFEBAR

        cameraZoom = FindObjectOfType<CameraZoom>();                                // CAMERAZOOM
        gameManager = GetComponent<GameManager>();                                  // GAMEMANAGER
        playerAction = FindObjectOfType<PlayerActions>();                           // PLAYERACTION
        cameraShake = FindObjectOfType<CameraShake>();                              // CAMERASHAKE

        chefText.text = "" + chefName;
    }

    // PLAYER LIFEBAR

    private void PlayerHealthBar()
    {
        float ratio = playerLife / playerMaxLife;
        playerHealth.rectTransform.localScale = new Vector3(ratio, 0.5f, 1);
    }

    // CHEF LIFEBAR

    private void ChefHealthBar()
    {
        float enemyratio = chefLife / chefMaxLife;
        chefHealth.rectTransform.localScale = new Vector3(enemyratio, 0.5f, 1);
    }

    // PRENDI DANNO

    public void TakeDamage()
    {
        playerAction.tastoParata.enabled = false;

        cameraShake.ShakeCamera(5, 0.5f);                                           // Shake Camera             
        playerLife -= chefDamage;                                                   // Take Damage

        playerHealth.transform.DOShakePosition(0.7f, 12f);                          // Shake the Player Image
        playerText.transform.DOShakePosition(0.7f, 12f);                            // Shake the Player Text

        // SCONFITTA

        if (playerLife <= 0)
        {
            playerLife = 0;
            gameManager.BlockCoroutine();                                           // Blocca (currentCouroutine)
            StartCoroutine(gameManager.LevelFailed());                              // SCONFITTA
        }

        PlayerHealthBar();                                                          // Aggiorna barra della vita del Player
    }

    // DANNI ALLO CHEF

    public void ChefDamage(string direction)
    {
        chefLife -= playerDamage;                                                   // Vita dello Chef - Danni del Player
        playerAction.chefAnimator.SetTrigger("TakeDamage");                         // Animazione danno allo Chef

        chefHealth.transform.DOShakePosition(0.7f, 12f);                            // Shake the Player Image
        chefText.transform.DOShakePosition(0.7f, 12f);                              // Shake the Player Text

        // PUGNI FINALI

        if (chefLife <= 0)
        {
            chefLife = 0;
            gameManager.BlockCoroutine();                                           // Blocca (currentCouroutine)
                                                           
            isFinalPunches = true;                                                  // Attiva Pugni Finali        
        }

        cameraZoom.ZoomIn(direction);                                               // Camera Zoom
        ChefHealthBar();                                                            // Aggiorna la LifeBar dello Chef
    }
}
