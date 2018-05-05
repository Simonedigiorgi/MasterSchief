using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PlayerActions : MonoBehaviour
{
    [HideInInspector] public LayerMask buttonMask;
    [HideInInspector] public LayerMask enemyMask;

    private HealthBar healthBar;                                                                            // HEALTBAR
    private SoundManager soundManager;                                                                      // SOUNDMANAGER
    private GameManager gameManager;                                                                        // GAMEMANAGER

    private Transform rightPos;                                                                             // Posizione Braccio Sinistro
    private Transform leftPos;                                                                              // Posizione Braccio Destro
    private Transform pointPos;                                                                             // Posizione Centrale

    private Vector3 leftStartingPos;                                                                        // Posizione iniziale Braccio Sinistro
    private Vector3 rightStartingPos;                                                                       // Posizione iniziale Braccio Destro

    [BoxGroup("Animators")] public Animator leftArmAnimator;                                                // Animazione Braccio Sinistro
    [BoxGroup("Animators")] public Animator rightArmAnimator;                                               // Animazione Braccio Destro
    [BoxGroup("Animators")] public Animator chefAnimator;                                                   // Animazione dello Chef

    [BoxGroup("Scritte Infami")] public GameObject parata;                                                  // Scritte infami (Parata)
    [BoxGroup("Scritte Infami")] public GameObject[] chefPunch;                                             // Scritte infami (Chef) - 2
    [BoxGroup("Scritte Infami")] public GameObject[] playerPunch;                                           // Scritte infami (Player) - 3

    private float parryTimer = 0;                                                                           // Lasciare a 0 
    private float canParryTimer = 0;                                                                        // Lasciare a 0 (Viene aumentato da un Time.deltatime)
    private float parryTime = 0.5f;                                                                         // Durata della parata
    private float parryCooldown = 0.8f;                                                                     // Tempo prima di parare ancora

    private bool isLevelComplete = false;                                                                   // Livello completato
    private bool isLevelFailed = false;                                                                     // Livello fallito
    private bool isToggle = false;                                                                          // Toggle di animazione tra Pungno sinistro/destro

    [BoxGroup(" Platform Controls")] public bool isController;                                              // Usa il Keyboard o il Controller Xbox
    [BoxGroup(" Platform Controls")] public bool isMouse;                                                   // Usa il Mouse

    [BoxGroup("Debug")] public bool isParrying = false;                                                     // Il Player sta parando
    [BoxGroup("Debug")] public bool isActive = false;                                                       // Attiva il Player

    [HideInInspector] public bool canParry = true;

    void Start()
    {
        /*isController = true;
        isMouse = true;*/

        leftPos = GameObject.Find("leftPos").transform;
        rightPos = GameObject.Find("rightPos").transform;
        pointPos = GameObject.Find("pointPos").transform;

        healthBar = FindObjectOfType<HealthBar>();
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
        canParryTimer = parryCooldown;

        leftStartingPos = leftArmAnimator.transform.position;
        rightStartingPos = rightArmAnimator.transform.position;
    }

    // Mostra testi infami dello Chef

    public void SpawnPunchInfame()
    {
        Instantiate(chefPunch[0], pointPos);
        soundManager.PlayCounter();
    }

    public void SpawnChargeInfame()
    {
        Instantiate(chefPunch[1], pointPos);
        soundManager.PlayCharged();
    }

    // Mostra testi infami del Player

    public void SpawnPlayerInfame()
    {
        if (Random.value > 0.5f)
        {
            GameObject s = Instantiate(playerPunch[Random.Range(0, playerPunch.Length)], rightPos);
            s.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
        else
        {
            GameObject s = Instantiate(playerPunch[Random.Range(0, playerPunch.Length)], leftPos);
            s.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }

        StartCoroutine(PunchSoundWithDelay());
    }

    // Mostra il Testo "Parat" (Para il colpo)

    public void SpawnParat()
    {
        Instantiate(parata, pointPos);
    }

    // Audio Pugni

    IEnumerator PunchSoundWithDelay()
    {
        yield return new WaitForSeconds(0.2f);

        soundManager.PlayPunchHits();

        if (healthBar.chefLife != 0)
        {
            yield return new WaitForSeconds(0.5f);
            if (Random.value > 0.5f)
                soundManager.PlayChefHits();
        }
    }

    // Audio Outro

    IEnumerator PlayOutroWithDelay()
    {
        yield return new WaitForSeconds(1);
        soundManager.PlayOutro();
    }

    void Update()
    {
        // SELEZIONA CONTROLLER || MOUSE

        /*if (isController)
        {
            isMouse = false;
            isController = true;
        }

        else if (isMouse)
        {
            isController = false;
            isMouse = true;
        }*/


        // CONTROLLI DEL GIOCATORE

        if (isActive == true)
        {
            // Xbox Controller

            if (isController)
            {
                if (isParrying == false && healthBar.playerLife != 0)
                {
                    // Left buttons

                    if (Input.GetButtonDown("Button Up/Left") && gameManager.buttonPunch[0].activeSelf)
                    {
                        leftArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[0].gameObject.SetActive(false);
                        healthBar.ChefDamage("left");
                        SpawnPlayerInfame();
                    }

                    if (Input.GetButtonDown("Button Middle/Left") && gameManager.buttonPunch[1].activeSelf)
                    {
                        leftArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[1].gameObject.SetActive(false);
                        healthBar.ChefDamage("left");
                        SpawnPlayerInfame();
                    }

                    if (Input.GetButtonDown("Button Down/Left") && gameManager.buttonPunch[2].activeSelf)
                    {
                        leftArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[2].gameObject.SetActive(false);
                        healthBar.ChefDamage("left");
                        SpawnPlayerInfame();
                    }

                    // Right buttons

                    if (Input.GetButtonDown("Button Up/Right") && gameManager.buttonPunch[3].activeSelf)
                    {
                        rightArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[3].gameObject.SetActive(false);
                        healthBar.ChefDamage("right");
                        SpawnPlayerInfame();
                    }

                    if (Input.GetButtonDown("Button Middle/Right") && gameManager.buttonPunch[4].activeSelf)
                    {
                        rightArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[4].gameObject.SetActive(false);
                        healthBar.ChefDamage("right");
                        SpawnPlayerInfame();
                    }

                    if (Input.GetButtonDown("Button Down/Right") && gameManager.buttonPunch[5].activeSelf)
                    {
                        rightArmAnimator.SetTrigger("Punch");
                        gameManager.buttonPunch[5].gameObject.SetActive(false);
                        healthBar.ChefDamage("right");
                        SpawnPlayerInfame();
                    }
                }

                if(healthBar.chefLife == 0 && isLevelComplete == false && isLevelFailed == false)
                {
                    if (healthBar.isFinalPunches == true)
                    {
                        if (Input.GetButtonDown("Button Down/Left"))
                        {
                            // Animazione Hit (Fase di pestaggio finale)

                            if (isToggle)
                            {
                                isToggle = !isToggle;
                                leftArmAnimator.SetTrigger("Punch");
                                SpawnPlayerInfame();
                            }
                            else
                            {
                                isToggle = !isToggle;
                                rightArmAnimator.SetTrigger("Punch");
                                SpawnPlayerInfame();
                            }

                            chefAnimator.SetTrigger("TakeDamage");
                            gameManager.clickCounter++;

                            healthBar.chefText.transform.DOShakePosition(0.7f, 12f);                              // Shake the Chef Text
                            healthBar.chefPanel.transform.DOShakePosition(0.7f, 12f);                             // Shake the Chef Bar

                            if (gameManager.clickCounter >= gameManager.finalPunches)
                            {
                                chefAnimator.SetTrigger("Rotto");
                                isLevelComplete = true;
                                StartCoroutine(PlayOutroWithDelay());
                            }

                        }
                    }
                }

                if (Input.GetButtonDown("Parry") && healthBar.playerLife != 0)
                {
                    Debug.Log("parry");
                    if (canParry)
                    {
                        isParrying = true;
                        canParry = false;
                        canParryTimer = 0;
                    }
                }
            }

            // Mouse Controls

            if (isMouse)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (healthBar.chefLife == 0 && isLevelComplete == false && isLevelFailed == false)
                    {
                        if (healthBar.isFinalPunches == true)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, enemyMask);

                            if (hit.collider != null)
                            {
                                // Animazione Hit (Fase di pestaggio finale)

                                if (isToggle)
                                {
                                    isToggle = !isToggle;
                                    leftArmAnimator.SetTrigger("Punch");
                                    SpawnPlayerInfame();
                                }
                                else
                                {
                                    isToggle = !isToggle;
                                    rightArmAnimator.SetTrigger("Punch");
                                    SpawnPlayerInfame();
                                }

                                chefAnimator.SetTrigger("TakeDamage");
                                gameManager.clickCounter++;

                                healthBar.chefText.transform.DOShakePosition(0.7f, 12f);                              // Shake the Chef Text
                                healthBar.chefPanel.transform.DOShakePosition(0.7f, 12f);                             // Shake the Chef Bar

                                if (gameManager.clickCounter >= gameManager.finalPunches)
                                {
                                    chefAnimator.SetTrigger("Rotto");
                                    isLevelComplete = true;
                                    StartCoroutine(PlayOutroWithDelay());
                                }

                            }
                        }
                    }
                    else if (isParrying == false && healthBar.playerLife != 0)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, buttonMask);

                        if (hit.collider != null)
                        {
                            // Comabattimento durante il gioco

                            if (hit.collider.tag == "ButtonLeft")
                            {
                                leftArmAnimator.SetTrigger("Punch");
                                hit.collider.gameObject.SetActive(false);
                                healthBar.ChefDamage("left");
                                SpawnPlayerInfame();

                            }
                            else if (hit.collider.tag == "ButtonRight")
                            {
                                rightArmAnimator.SetTrigger("Punch");
                                hit.collider.gameObject.SetActive(false);
                                healthBar.ChefDamage("right");
                                SpawnPlayerInfame();
                            }

                        }
                        else
                        {
                            chefAnimator.SetTrigger("Punch");
                        }
                    }
                }
                else if (Input.GetMouseButtonDown(1) && healthBar.playerLife != 0)
                {
                    if (canParry)
                    {
                        isParrying = true;
                        canParry = false;
                        canParryTimer = 0;
                    }
                }
            }
        }

        // ANIMAZIONE E CONTROLLO PARATA

        if (isParrying == true)
        {
            parryTimer += Time.deltaTime;

            if (parryTimer >= parryTime)
            {
                isParrying = false;
                parryTimer = 0;
            }

            leftArmAnimator.transform.position = leftStartingPos + new Vector3(1, 0, 0);
            leftArmAnimator.transform.rotation = Quaternion.Euler(0, 0, -20);

            rightArmAnimator.transform.position = rightStartingPos - new Vector3(1, 0, 0);
            rightArmAnimator.transform.rotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            leftArmAnimator.transform.position = leftStartingPos;
            leftArmAnimator.transform.rotation = Quaternion.Euler(0, 0, 0);

            rightArmAnimator.transform.position = rightStartingPos;
            rightArmAnimator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        leftArmAnimator.SetBool("Parry", isParrying);
        rightArmAnimator.SetBool("Parry", isParrying);

        canParryTimer += Time.deltaTime;

        if (canParryTimer > parryCooldown)
        {
            canParry = true;
        }

        // LIVELLO COMPLETATO

        if (isLevelComplete == true)
        {
            StartCoroutine(gameManager.LevelComplete());
        }

        // LIVELLO FALLITO

        if (isLevelFailed == true)
        {
            chefAnimator.Play("Idle");
            isActive = false;

            StartCoroutine(gameManager.LevelFailed());
        }
    }
}

