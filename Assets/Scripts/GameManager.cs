using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public IEnumerator currentCoroutine;                                                            // COROUTINE

    private PlayerActions playerAction;                                                             // PLAYERACTION
    private EventScript eventScript;                                                                // EVENTSCRIPT
    private HealthBar healthBar;                                                                    // HEALTHBAR
    private SoundManager soundManager;                                                              // SOUNDMANAGER

    public GameObject[] buttonPunch;                                                                // Array dei Tasti

    [BoxGroup("Animator")] public Animator chefAnimator;                                            // CHEFANIMATOR

    [BoxGroup("Immagini")] public Image fade;                                                       // Immagine di Fade

    [BoxGroup("Controlli")] public float secondsBeforeStart;                                        // Secondi del primo attacco dello Chef (Inizio Partita)
    [BoxGroup("Controlli")] public string nextScene = "";                                           // Prossima scena

    [BoxGroup("Controller dei Tasti")] public float delayBetweenButtons = 1;                        // Tempo tra un tasto e l'altro
    [BoxGroup("Controller dei Tasti")] public float buttonActiveTime = 1;                           // Quanto tempo i Bottoni rimangono a schermo prima di sparire

    [BoxGroup("Dimensione dei Tasti")] [Range(1, 7)] public int minButtonSpawnScale;                // Scala minima dei bottoni
    [BoxGroup("Dimensione dei Tasti")] [Range(1, 7)] public int maxButtonSpawnScale;                // Scala massima dei bottoni

    [BoxGroup("Controller dei Tasti")] [Range(0, 1)] public float buttonEventChance;                // (Più vicino allo 0 aumenta la possibilità di Attacco dello Chef)
    [BoxGroup("Controller dei Tasti")] [Range(1, 6)] public int maxButtonSpawnAmountPerEvent;       // Quanti tasti posso apparire insieme

    [BoxGroup("Pestata Finale")] public int clickCounter = 0;                                       // Conteggio dei Pugni finali
    [BoxGroup("Pestata Finale")] public int finalPunches;                                           // Click della scazzottata finale


    private void Start()
    {
        fade.enabled = true;
        fade.DOFade(0, 0);

        playerAction = FindObjectOfType<PlayerActions>();
        healthBar = FindObjectOfType<HealthBar>();
        eventScript = FindObjectOfType<EventScript>();
        soundManager = FindObjectOfType<SoundManager>();

        // Disattiva i Bottoni

        foreach (GameObject button in buttonPunch)
        {
            button.SetActive(false);
        }

        StartCoroutine(SecondsBeforeStart());
    }

    public IEnumerator SecondsBeforeStart()
    {
        soundManager.PlayGetReady();

        yield return new WaitForSeconds(4);

        soundManager.PlayIntro();
        playerAction.isActive = true;

        yield return new WaitForSeconds(secondsBeforeStart);
        if (Random.value <= buttonEventChance)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);
        }
    }

    public IEnumerator YouPunch()
    {
        float nextCoroutine = 0;
        int value = Random.Range(1, maxButtonSpawnAmountPerEvent);

        for(int i = 0; i < value; i++)
        {
            GetRandomButton().SetActive(true);
            yield return new WaitForSeconds(delayBetweenButtons);
        }
        
        nextCoroutine += delayBetweenButtons > buttonActiveTime ? 0 : buttonActiveTime - delayBetweenButtons;

        yield return new WaitForSeconds(nextCoroutine);

        if (Random.value <= buttonEventChance)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);
        }

    }

    public GameObject GetRandomButton()
    {
        int index = Random.Range(0, buttonPunch.Length);
        while(buttonPunch[index].activeSelf)
        {
            index = Random.Range(0, buttonPunch.Length);
        }

        // Dimesione dei Tasti

        float size = Random.Range(minButtonSpawnScale, maxButtonSpawnScale);
        buttonPunch[index].transform.localScale = new Vector3(size, size, 1);
        return buttonPunch[index];
    }

    public IEnumerator YouParry()
    {
        yield return null;
        chefAnimator.SetTrigger("ChargePunch");        
    }

    public IEnumerator CheckIfParrying()
    {
        bool trigger = false;
        bool trigger2 = false;

        if (playerAction.isParrying && !trigger2)
        {
            soundManager.PlayCharged();
            trigger2 = true;
            playerAction.SpawnParat();
        }
        else if (!trigger)
        {
            trigger = true;
            healthBar.TakeDamage();
            playerAction.SpawnChargeInfame();
        }

        while (!playerAction.canParry)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        if (Random.value <= buttonEventChance)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);

        }
    }

    public void BlockCoroutine()
    {
        //Debug.Log(currentCoroutine.ToString());
        StopCoroutine(currentCoroutine);
    }

    // COROUTINES

    public IEnumerator LevelFailed()
    {
        yield return new WaitForSeconds(1);
        fade.DOFade(1, 3);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1);
        fade.DOFade(1, 3);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextScene);
    }


}
