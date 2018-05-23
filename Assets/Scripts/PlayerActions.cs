using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PlayerActions : MonoBehaviour
{
    [HideInInspector] public LayerMask buttonMask;
    [HideInInspector] public LayerMask enemyMask;

    private HealthBar healthBar;                                                                            // HEALTBAR
    private SoundManager soundManager;                                                                      // SOUNDMANAGER
    private GameManager gameManager;                                                                        // GAMEMANAGER
    private FinalPunches finalPunches;                                                                      // FINALPUNCHES
    private PlayerSettings settings;                                                                        // PLAYER SETTINGS

    private Transform rightPos;                                                                             // Posizione Braccio Sinistro
    private Transform leftPos;                                                                              // Posizione Braccio Destro
    private Transform pointPos;                                                                             // Posizione Centrale

    private Vector3 leftStartingPos;                                                                        // Posizione iniziale Braccio Sinistro
    private Vector3 rightStartingPos;                                                                       // Posizione iniziale Braccio Destro

    private Animator leftAnim;                                                                              // Animazione Braccio Sinistro
    private Animator rightAnim;                                                                             // Animazione Braccio Destro

    [HideInInspector] public Animator chefAnimator;                                                         // Animazione dello Chef

    [BoxGroup("Scritte Infami")] public GameObject[] chefPunch;                                             // Scritte infami (Chef) - 2
    [BoxGroup("Scritte Infami")] public GameObject[] playerPunch;                                           // Scritte infami (Player) - 3
    [BoxGroup("Scritte Infami")] public GameObject parata;                                                  // Scritte infami (Parata)

    private float parryTimer = 0;                                                                           // Lasciare a 0 
    private float canParryTimer = 0;                                                                        // Lasciare a 0 (Viene aumentato da un Time.deltatime)
    private float parryTime = 0.5f;                                                                         // Durata della parata
    private float parryCooldown = 0.8f;                                                                     // Tempo prima di parare ancora

    private bool isLevelComplete = false;                                                                   // Livello completato
    [HideInInspector] public bool isLevelFailed = false;                                                    // Livello fallito
    private bool isToggle = false;                                                                          // Toggle di animazione tra Pungno sinistro/destro

    [HideInInspector] public bool isParrying = false;                                                       // Il Player sta parando
    [HideInInspector] public bool isActive = false;                                                         // Attiva il Player

    [HideInInspector] public bool canParry = true;                                                          // Può parare?
    [HideInInspector] public bool canFinalPunches;                                                          // Se vera puoi iniziare a colpire lo Chef nella parte finale del gioco

    public Image tastoParata;

    public void Awake()
    {
        // LOAD DATA

        settings = FindObjectOfType<PlayerSettings>();
        settings.LoadData();
    }

    void Start()
    {
        leftAnim = transform.GetChild(0).GetComponent<Animator>();
        rightAnim = transform.GetChild(1).GetComponent<Animator>();

        chefAnimator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();

        leftPos = GameObject.Find("leftPos").transform;
        rightPos = GameObject.Find("rightPos").transform;
        pointPos = GameObject.Find("pointPos").transform;

        healthBar = FindObjectOfType<HealthBar>();
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
        finalPunches = FindObjectOfType<FinalPunches>();

        canParryTimer = parryCooldown;

        leftStartingPos = leftAnim.transform.position;
        rightStartingPos = rightAnim.transform.position;
    }

    void Update()
    {
        // CONTROLLI DEL GIOCATORE

        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (healthBar.chefLife == 0 && isLevelComplete == false && isLevelFailed == false)
                {
                    if (healthBar.isFinalPunches == true)
                    {
                        StartCoroutine(WaitBeforeFinalPunches());
                        if (canFinalPunches)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)), Camera.main.transform.forward, enemyMask);

                            if (hit.collider != null)
                            {
                                // Animazione Hit (Fase di pestaggio finale)

                                if (isToggle)
                                {
                                    isToggle = !isToggle;
                                    leftAnim.SetTrigger("Punch");
                                    SpawnPlayerInfame();
                                }
                                else
                                {
                                    isToggle = !isToggle;
                                    rightAnim.SetTrigger("Punch");
                                    SpawnPlayerInfame();
                                }

                                chefAnimator.SetTrigger("TakeDamage");
                                finalPunches.clickCounter++;

                                healthBar.chefText.transform.DOShakePosition(0.7f, 12f);                                   // Shake the Chef Text
                                healthBar.chefPanel.transform.DOShakePosition(0.7f, 12f);                                  // Shake the Chef Bar

                                finalPunches.counterText.GetComponent<Animation>().Play("ScaleIn_CounterText");            // Animate the Counter Text

                                if (finalPunches.clickCounter >= finalPunches.punches)
                                {
                                    chefAnimator.SetTrigger("Rotto");
                                    isLevelComplete = true;
                                    StartCoroutine(PlayOutroWithDelay());
                                }
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
                            hit.collider.gameObject.SetActive(false);
                            LeftPunch();
                        }
                        else if (hit.collider.tag == "ButtonRight")
                        {
                            hit.collider.gameObject.SetActive(false);
                            RightPunch();
                        }
                        else if (hit.collider.tag == "Enemy" && !isLevelComplete)
                        {
                            if (healthBar.playerLife != 0)
                            {
                                if (canParry)
                                    Parry();
                            }
                        }
                    }
                    else
                        chefAnimator.SetTrigger("Punch");
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

            leftAnim.transform.position = leftStartingPos + new Vector3(1, 0, 0);
            leftAnim.transform.rotation = Quaternion.Euler(0, 0, -20);

            rightAnim.transform.position = rightStartingPos - new Vector3(1, 0, 0);
            rightAnim.transform.rotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            leftAnim.transform.position = leftStartingPos;
            leftAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

            rightAnim.transform.position = rightStartingPos;
            rightAnim.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        leftAnim.SetBool("Parry", isParrying);
        rightAnim.SetBool("Parry", isParrying);

        canParryTimer += Time.deltaTime;

        if (canParryTimer > parryCooldown)
        {
            canParry = true;
        }

        // LIVELLO COMPLETATO

        if (isLevelComplete)
        {
            StartCoroutine(gameManager.LevelComplete());
            finalPunches.pressButtonImage.GetComponent<Animation>().Stop("PressButton");
        }

        // LIVELLO FALLITO

        if (isLevelFailed)
        {
            chefAnimator.Play("Idle");
            isActive = false;

            StartCoroutine(gameManager.LevelFailed());
        }
    }

    // COROUTINES

    public IEnumerator WaitBeforeFinalPunches()
    {
        yield return new WaitForSeconds(2.5f);
        canFinalPunches = true;
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

    // METHODS

    public void LeftPunch()
    {
        leftAnim.SetTrigger("Punch");
        healthBar.ChefDamage("left");
        SpawnPlayerInfame();
    }

    public void RightPunch()
    {
        rightAnim.SetTrigger("Punch");
        healthBar.ChefDamage("right");
        SpawnPlayerInfame();
    }

    public void Parry()
    {
        isParrying = true;
        canParry = false;
        canParryTimer = 0;
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
        tastoParata.enabled = false;
    }
}

